using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreBusiness.DTOs;
using CoreBusiness.Interfaces;
using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.SQL.Utilities;

namespace Plugins.DataStore.SQL;

public class BaseSqlRepository
{
    protected ApplicationDbContext Context { get; }
    protected IMapper Mapper { get; }

    public BaseSqlRepository(ApplicationDbContext context, IMapper mapper)
    {
        Context = context;
        Mapper = mapper;
    }

    protected async Task<int> Count<TEntity>() 
        where TEntity: class
    {
        return await Context.Set<TEntity>().CountAsync();
    }

    protected async Task<bool> Exist<TEntity>(int id)
        where TEntity: class, IId
    {
        return await Context.Set<TEntity>().AnyAsync(entity => entity.Id == id);
    }
    
    protected async Task<List<TDto>> Get<TEntity, TDto>(
        Expression<Func<TEntity, object>> orderBy,
        Expression<Func<TEntity, bool>>? where = null,
        PaginationDto? paginationDto = null,
        int top = 0
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

    protected async Task<TDto?> Get<TEntity, TDto>(int id)
        where TEntity: class 
        where TDto: IId
    {
        return await Context.Set<TEntity>()
            .ProjectTo<TDto>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(dto => dto.Id == id);
    }

    protected async Task<TDto> Add<TEntity, TCreationDto, TDto>(TCreationDto creationDto)
        where TEntity: class
    {
        var entity = Mapper.Map<TEntity>(creationDto);
        Context.Add(entity);
        await Context.SaveChangesAsync();

        return Mapper.Map<TDto>(entity);
    }

    protected async Task<bool> Update<TEntity, TCreationDto>(int id, TCreationDto creationDto)
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

    protected async Task<bool> Delete<TEntity>(int id)
        where TEntity: class, IId
    {
        var deleteRecords = await Context.Set<TEntity>().Where(entity => entity.Id == id).ExecuteDeleteAsync();

        return deleteRecords != 0;
    }
}