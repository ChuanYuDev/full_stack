using CoreBusiness;
using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface ITheatersRepository: IRepository<Theater, TheaterCreationDto, TheaterDto>
{
}