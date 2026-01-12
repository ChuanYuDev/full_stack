using CoreBusiness;
using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IGenresRepository
{
    Task<List<GenreDto>> GetAll();
    Task<Genre?> GetById(int id);
    bool Exists(string name);
    Task<GenreDto> Add(GenreCreationDto genreCreationDto);
}