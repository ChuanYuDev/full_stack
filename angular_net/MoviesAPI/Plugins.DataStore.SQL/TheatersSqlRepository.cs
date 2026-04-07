using System.Linq.Expressions;
using AutoMapper;
using CoreBusiness;
using CoreBusiness.DTOs;
using UseCases.DataStoreInterfaces;

namespace Plugins.DataStore.SQL;

public class TheatersSqlRepository: SqlRepository<Theater, TheaterCreationDto, TheaterDto>, ITheatersRepository
{
    public TheatersSqlRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<TheaterDto>> Get(Expression<Func<Theater, bool>>? where = null, int top = 0)
    {
        return await Get(where: where, orderBy:t => t.Name, top: top);
    }

    public async Task<List<TheaterDto>> Get(PaginationDto paginationDto)
    {
        return await Get(paginationDto, t => t.Name);
    }
}