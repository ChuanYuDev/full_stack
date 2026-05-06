using AutoMapper;
using CoreBusiness;
using CoreBusiness.DTOs;
using Microsoft.EntityFrameworkCore;
using UseCases.DataStoreInterfaces;

namespace Plugins.DataStore.SQL;

public class RatingsRepository : IRatingsRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IUsersRepository _usersRepository;
    private readonly IMoviesRepository _moviesRepository;
    private readonly IMapper _mapper;

    public RatingsRepository(ApplicationDbContext context, IUsersRepository usersRepository, IMoviesRepository moviesRepository, IMapper mapper)
    {
        _context = context;
        _usersRepository = usersRepository;
        _moviesRepository = moviesRepository;
        _mapper = mapper;
    }

    public async Task<int> GetRate(int movieId)
    {
        var result = await GetUserIdAndMovieRating(movieId);

        return result.movieRating?.Rate ?? 0;
    }

    public async Task<bool> AddOrUpdate(MovieRatingCreationDto movieRatingCreationDto)
    {
        var movieExist = await _moviesRepository.Exist(movieRatingCreationDto.MovieId);

        if (!movieExist)
        {
            return false;
        }

        var result = await GetUserIdAndMovieRating(movieRatingCreationDto.MovieId);

        if (result.movieRating is null)
        {
            var movieRating = _mapper.Map<MovieRating>(movieRatingCreationDto);
            movieRating.UserId = result.userId;
            
            _context.Add(movieRating);
        }
        else
        {
            _mapper.Map(movieRatingCreationDto, result.movieRating);
        }

        await _context.SaveChangesAsync();
        return true;
    }

    private async Task<(string userId, MovieRating? movieRating)> GetUserIdAndMovieRating(int movieId)
    {
        var userId = await _usersRepository.GetUserId();

        var movieRating = await _context.MovieRatings.FirstOrDefaultAsync(mr => mr.MovieId == movieId && mr.UserId == userId);

        return (userId, movieRating);
    }
}