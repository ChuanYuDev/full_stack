using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreBusiness.DTOs;
using CoreBusiness.Interfaces;
using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.SQL.Utilities;

namespace Plugins.DataStore.SQL;

public class BaseSqlRepository<TEntity, TCreationDto, TDto, TDetailsDto> where TDetailsDto: TDto
    where TEntity: class, IId
    where TDto: IId
{
    protected ApplicationDbContext Context { get; }
    protected DbSet<TEntity> EntityDbSet { get; }
    protected IMapper Mapper { get; }

    public BaseSqlRepository(ApplicationDbContext context, IMapper mapper)
    {
        Context = context;
        EntityDbSet = Context.Set<TEntity>();
        Mapper = mapper;
    }

    public async Task<int> Count()
    {
        return await EntityDbSet.CountAsync();
    }
    
    protected async Task<List<TDto>> Get<TKey>(Expression<Func<TEntity, bool>>? where, Expression<Func<TEntity, TKey>> orderBy, int top)
    {
        var queryable = EntityDbSet.AsQueryable();

        if (where is not null)
        {
            queryable = queryable.Where(where);
        }

        queryable = queryable.OrderBy(orderBy);

        if (top != 0)
        {
            queryable = queryable.Take(top);
        }
        
        return await queryable
            .ProjectTo<TDto>(Mapper.ConfigurationProvider)
            .ToListAsync();
    }

    protected async Task<List<TDto>> Get<TKey>(PaginationDto paginationDto, Expression<Func<TEntity, TKey>> orderBy)
    {
        return await EntityDbSet 
            .OrderBy(orderBy)
            .Paginate(paginationDto)
            .ProjectTo<TDto>(Mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public virtual async Task<TDetailsDto?> Get(int id)
    {
        return await EntityDbSet
            .ProjectTo<TDetailsDto>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(dto => dto.Id == id);
    }

    public virtual async Task<TDto> Add(TCreationDto creationDto)
    {
        var entity = Mapper.Map<TEntity>(creationDto);
        Context.Add(entity);
        await Context.SaveChangesAsync();

        return Mapper.Map<TDto>(entity);
    }

    public virtual async Task<bool> Update(int id, TCreationDto creationDto)
    {
        var found = await EntityDbSet.AnyAsync(entity => entity.Id == id);

        if (!found)
        {
            return false;
        }

        var entity = Mapper.Map<TEntity>(creationDto);
        entity.Id = id;

        Context.Update(entity);
        await Context.SaveChangesAsync();

        return true;
    }

    public virtual async Task<bool> Delete(int id)
    {
        var deleteRecords = await EntityDbSet.Where(entity => entity.Id == id).ExecuteDeleteAsync();

        return deleteRecords != 0;
    }
}