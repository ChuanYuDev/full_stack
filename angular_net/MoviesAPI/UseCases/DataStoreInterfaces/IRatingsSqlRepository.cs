using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IRatingsSqlRepository
{
    Task<int> GetRate(int movieId);
    
    Task<bool> AddOrUpdate(MovieRatingCreationDto movieRatingCreationDto);
}