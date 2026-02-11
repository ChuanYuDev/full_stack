using CoreBusiness;
using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IGenresRepository
{
    Task<int> Count();
    Task<bool> Exists(int id);
    Task<List<GenreDto>> GetAll();
    Task<List<GenreDto>> Get(PaginationDto paginationDto);
    Task<GenreDto?> GetById(int id);
    Task<GenreDto> Add(GenreCreationDto genreCreationDto);
    Task Update(int id, GenreCreationDto genreCreationDto);
    Task<int> Delete(int id);
}