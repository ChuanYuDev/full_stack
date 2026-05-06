using System.Linq.Expressions;
using AutoMapper;
using CoreBusiness;
using CoreBusiness.DTOs;
using UseCases.DataStoreInterfaces;

namespace Plugins.DataStore.SQL;

public class TheatersSqlRepository: BaseSqlRepository, ITheatersRepository
{
    public TheatersSqlRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<int> Count()
    {
        return await Count<Theater>();
    }

    public async Task<List<TheaterDto>> Get()
    {
        return await Get<Theater, TheaterDto>(orderBy: theater => theater.Name);
    }

    public async Task<List<TheaterDto>> Get(Expression<Func<Theater, bool>> where)
    {
        return await Get<Theater, TheaterDto>(orderBy: theater => theater.Name, where: where);
    }

    public async Task<List<TheaterDto>> Get(PaginationDto paginationDto)
    {
        return await Get<Theater, TheaterDto>(orderBy: theater => theater.Name, paginationDto: paginationDto);
    }

    public async Task<TheaterDto?> Get(int id)
    {
        return await Get<Theater, TheaterDto>(id);
    }

    public async Task<TheaterDto> Add(TheaterCreationDto theaterCreationDto)
    {
        return await Add<Theater, TheaterCreationDto, TheaterDto>(theaterCreationDto);
    }

    public async Task<bool> Update(int id, TheaterCreationDto theaterCreationDto)
    {
        return await Update<Theater, TheaterCreationDto>(id, theaterCreationDto);
    }

    public async Task<bool> Delete(int id)
    {
        return await Delete<Theater>(id);
    }
}