using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IGenresRepository: IRepository<GenreCreationDto, GenreDto>
{
}