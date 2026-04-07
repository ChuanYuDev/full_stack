using System.Linq.Expressions;
using CoreBusiness;
using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IMoviesRepository: IBaseRepository<Movie, MovieCreationDto, MovieDto, MovieDetailsDto>
{
}