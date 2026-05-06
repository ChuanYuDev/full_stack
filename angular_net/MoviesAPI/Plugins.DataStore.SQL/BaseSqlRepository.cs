using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreBusiness.DTOs;
using CoreBusiness.Interfaces;
using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.SQL.Utilities;

namespace Plugins.DataStore.SQL;

// public class BaseSqlRepository<TEntity, TCreationDto, TDto, TDetailsDto>
//     where TDetailsDto: TDto
//     where TEntity: class, IId
//     where TDto: IId
//     where TEntity: class
public class BaseSqlRepository
{
    protected ApplicationDbContext Context { get; }
    protected IMapper Mapper { get; }

    public BaseSqlRepository(ApplicationDbContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    public async Task<int> Count<TEntity>() 
        where TEntity: class
    {
        return await Context.Set<TEntity>().CountAsync();
    }

    public async Task<bool> Exist<TEntity>(int id)
        where TEntity: class, IId
    {
        return await Context.Set<TEntity>().AnyAsync(entity => entity.Id == id);
    }
    
    protected async Task<List<TDto>> Get<TEntity, TKey, TDto>(
        Expression<Func<TEntity, bool>>? where,
        Expression<Func<TEntity, TKey>> orderBy,
        PaginationDto? paginationDto,
        int top
    )
        where TEntity: class
    {
        var queryable = Context.Set<TEntity>().AsQueryable();

        if (where is not null)
        {
            queryable = queryable.Where(where);
        }

        queryable = queryable.OrderBy(orderBy);

        if (paginationDto is not null)
        {
            
            queryable = queryable.Paginate(paginationDto);
        }

        if (top != 0)
        {
            queryable = queryable.Take(top);
        }
        
        return await queryable
            .ProjectTo<TDto>(Mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public virtual async Task<TDto?> Get<TEntity, TDto>(int id)
        where TEntity: class 
        where TDto: IId
    {
        return await Context.Set<TEntity>()
            .ProjectTo<TDto>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(dto => dto.Id == id);
    }

    public virtual async Task<TDto> Add<TEntity, TCreationDto, TDto>(TCreationDto creationDto)
    {
        var entity = Mapper.Map<TEntity>(creationDto);
        Context.Add(entity);
        await Context.SaveChangesAsync();

        return Mapper.Map<TDto>(entity);
    }

    public virtual async Task<bool> Update<TEntity, TCreationDto>(int id, TCreationDto creationDto)
        where TEntity: class, IId
    {
        var found = await Exist<TEntity>(id);

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

    public virtual async Task<bool> Delete<TEntity>(int id)
        where TEntity: class, IId
    {
        var deleteRecords = await Context.Set<TEntity>().Where(entity => entity.Id == id).ExecuteDeleteAsync();

        return deleteRecords != 0;
    }
}