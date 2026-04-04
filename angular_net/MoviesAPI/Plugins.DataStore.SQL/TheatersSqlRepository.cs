using AutoMapper;
using CoreBusiness;
using CoreBusiness.DTOs;
using UseCases.DataStoreInterfaces;

namespace Plugins.DataStore.SQL;

public class TheatersSqlRepository: CustomBaseSqlRepository<Theater, TheaterCreationDto, TheaterDto>, ITheatersRepository
{
    public TheatersSqlRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<TheaterDto>> Get()
    {
        return await Get(t => t.Name);
    }

    public async Task<List<TheaterDto>> Get(PaginationDto paginationDto)
    {
        return await Get(paginationDto, t => t.Name);
    }
}