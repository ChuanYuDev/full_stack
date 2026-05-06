using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IRatingsRepository
{
    Task<int> GetRate(int movieId);
    
    Task<bool> AddOrUpdate(MovieRatingCreationDto movieRatingCreationDto);
}