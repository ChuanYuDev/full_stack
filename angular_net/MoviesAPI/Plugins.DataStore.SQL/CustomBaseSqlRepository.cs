using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreBusiness.DTOs;
using CoreBusiness.Interfaces;
using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.SQL.Utilities;

namespace Plugins.DataStore.SQL;

public class CustomBaseSqlRepository<TEntity, TCreationDto, TDto>
    where TEntity: class, IId
    where TDto: IId
{
    protected ApplicationDbContext Context { get; }
    protected DbSet<TEntity> EntityDbSet { get; }
    protected IMapper Mapper { get; }

    public CustomBaseSqlRepository(ApplicationDbContext context, IMapper mapper)
    {
        Context = context;
        EntityDbSet = Context.Set<TEntity>();
        Mapper = mapper;
    }

    public async Task<int> Count()
    {
        return await EntityDbSet.CountAsync();
    }
    
    protected async Task<List<TDto>> GetAll(Expression<Func<TEntity, object>> orderBy)
    {
        return await EntityDbSet 
            .OrderBy(orderBy)
            .ProjectTo<TDto>(Mapper.ConfigurationProvider)
            .ToListAsync();
    }

    protected async Task<List<TDto>> Get(PaginationDto paginationDto, Expression<Func<TEntity, object>> orderBy)
    {
        return await EntityDbSet 
            .OrderBy(orderBy)
            .Paginate(paginationDto)
            .ProjectTo<TDto>(Mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<TDto?> GetById(int id)
    {
        return await EntityDbSet
            .ProjectTo<TDto>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(entityDto => entityDto.Id == id);
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