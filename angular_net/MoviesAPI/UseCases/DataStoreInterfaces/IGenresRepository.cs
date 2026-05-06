using System.Linq.Expressions;
using CoreBusiness;
using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IGenresRepository
{
    Task<int> Count();
    Task<List<GenreDto>> Get();
    Task<List<GenreDto>> Get(Expression<Func<Theater, bool>> where);
    Task<List<GenreDto>> Get(PaginationDto paginationDto);
    Task<GenreDto?> Get(int id);
    Task<GenreDto> Add(GenreCreationDto genreCreationDto);
    Task<bool> Update(int id, GenreCreationDto genreCreationDto);
    Task<bool> Delete(int id);
}