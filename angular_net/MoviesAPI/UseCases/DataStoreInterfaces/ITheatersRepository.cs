using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface ITheatersRepository: IRepository<TheaterCreationDto, TheaterDto>
{
}