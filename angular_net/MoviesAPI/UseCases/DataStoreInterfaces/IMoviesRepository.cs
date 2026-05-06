using System.Linq.Expressions;
using CoreBusiness;
using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IMoviesRepository
{
    Task<bool> Exist(int id);
    Task<List<MovieDto>> Get(Expression<Func<Movie, bool>> where, int top);
    Task<(List<MovieDto> Movies, int MoviesNum)> Filter(MoviesFilterDto moviesFilterDto);
    Task<MovieDetailsDto?> Get(int id);
    Task<MovieDto> Add(MovieCreationDto movieCreationDto);
    Task<bool> Update(int id, MovieCreationDto movieCreationDto);
    Task<bool> Delete(int id);
}