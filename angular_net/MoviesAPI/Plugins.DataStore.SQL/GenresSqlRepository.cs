using System.Linq.Expressions;
using AutoMapper;
using CoreBusiness;
using CoreBusiness.DTOs;
using UseCases.DataStoreInterfaces;

namespace Plugins.DataStore.SQL;

public class GenresSqlRepository: BaseSqlRepository, IGenresRepository 
{
    public GenresSqlRepository(ApplicationDbContext context, IMapper mapper): base(context, mapper)
    {
    }

    public async Task<int> Count()
    {
        return await Count<Genre>();
    }

    public async Task<List<GenreDto>> Get()
    {
        return await Get<Genre, GenreDto>(orderBy: genre => genre.Name);
    }

    public async Task<List<GenreDto>> Get(Expression<Func<Genre, bool>> where)
    {
        return await Get<Genre, GenreDto>(orderBy: genre => genre.Name, where: where);
    }

    public async Task<List<GenreDto>> Get(PaginationDto paginationDto)
    {
        return await Get<Genre, GenreDto>(orderBy: g => g.Name, paginationDto: paginationDto);
    }

    public async Task<GenreDto?> Get(int id)
    {
        return await Get<Genre, GenreDto>(id);
    }

    public async Task<GenreDto> Add(GenreCreationDto genreCreationDto)
    {
        return await Add<Genre, GenreCreationDto, GenreDto>(genreCreationDto);
    }

    public async Task<bool> Update(int id, GenreCreationDto genreCreationDto)
    {
        return await Update<Genre, GenreCreationDto>(id, genreCreationDto);
    }

    public async Task<bool> Delete(int id)
    {
        return await Delete<Genre>(id);
    }
}