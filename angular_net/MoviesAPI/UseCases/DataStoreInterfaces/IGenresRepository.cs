using CoreBusiness;
using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IGenresRepository: IRepository<Genre, GenreCreationDto, GenreDto>
{
}