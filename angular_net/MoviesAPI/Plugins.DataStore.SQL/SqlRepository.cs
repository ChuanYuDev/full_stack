using AutoMapper;
using CoreBusiness.Interfaces;

namespace Plugins.DataStore.SQL;

public class SqlRepository<TEntity, TCreationDto, TDto>: BaseSqlRepository<TEntity, TCreationDto, TDto, TDto>
    where TEntity: class, IId
    where TDto: IId
{
    public SqlRepository(ApplicationDbContext context, IMapper mapper): base(context, mapper)
    {}
}