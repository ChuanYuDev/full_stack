using System.Linq.Expressions;
using CoreBusiness;
using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IMoviesRepository
{
    Task<List<MovieDto>> Get(Expression<Func<Movie, Boolean>> where, int top);

    Task<MovieDetailsDto?> Get(int id);

    Task<MovieDto> Add(MovieCreationDto movieCreationDto);

}