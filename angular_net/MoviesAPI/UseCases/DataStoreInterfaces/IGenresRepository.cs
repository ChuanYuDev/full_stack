using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IGenresRepository
{
    Task<int> Count();
    Task<List<GenreDto>> GetAll();
    Task<List<GenreDto>> Get(PaginationDto paginationDto);
    Task<GenreDto?> GetById(int id);
    Task<GenreDto> Add(GenreCreationDto genreCreationDto);
    Task<bool> Update(int id, GenreCreationDto genreCreationDto);
    Task<int> Delete(int id);
}