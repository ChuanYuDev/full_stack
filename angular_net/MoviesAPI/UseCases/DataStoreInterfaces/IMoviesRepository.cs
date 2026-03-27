using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IMoviesRepository: IRepository<MovieCreationDto, MovieDto>
{
    
}