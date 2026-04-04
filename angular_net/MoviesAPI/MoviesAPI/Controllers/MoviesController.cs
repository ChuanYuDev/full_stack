using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

[Route("api/movies")]
[ApiController]
public class MoviesController: ControllerBase
{
    private readonly IGenresRepository _genresRepository;
    private readonly ITheatersRepository _theatersRepository;
    private readonly IMoviesRepository _moviesRepository;
    private readonly IOutputCacheStore _outputCacheStore;
    private const string CacheTag = "movies";
    private const string GetByIdName = "GetMovieById";

    public MoviesController(
        IGenresRepository genresRepository,
        ITheatersRepository theatersRepository,
        IMoviesRepository moviesRepository,
        IOutputCacheStore outputCacheStore
    )
    {
        _genresRepository = genresRepository;
        _theatersRepository = theatersRepository;
        _moviesRepository = moviesRepository;
        _outputCacheStore = outputCacheStore;
    }

    [HttpGet("post-get")]
    public async Task<ActionResult<MoviePostGetDto>> PostRead()
    {
        var genres = await _genresRepository.Get();
        var theaters = await _theatersRepository.Get();

        return new MoviePostGetDto { Genres = genres, Theaters = theaters };
    }

    [HttpGet("landing")]
    public async Task<ActionResult<LandingDto>> Get()
    {
        var today = DateTime.Today;
        var top = 6;

        var inTheaters = await _moviesRepository.Get(where: m => m.MoviesTheaters.Count > 0, top);
        var upcomingReleases = await _moviesRepository.Get(where: m => m.ReleaseDate > today, top);

        return new LandingDto { InTheaters = inTheaters, UpcomingReleases = upcomingReleases };
    }

    [HttpGet("{id:int}", Name = GetByIdName)]
    [OutputCache(Tags = [CacheTag])]
    public async Task<ActionResult<MovieDetailsDto>> Get(int id)
    {
        var movieDetailsDto = await _moviesRepository.Get(id);

        if (movieDetailsDto is null)
        {
            return NotFound();
        }

        return movieDetailsDto;
    }

    [HttpPost]
    public async Task<CreatedAtRouteResult> Post([FromForm] MovieCreationDto movieCreationDto)
    {
        var movieDto = await _moviesRepository.Add(movieCreationDto);
        await _outputCacheStore.EvictByTagAsync(CacheTag, default);
        return CreatedAtRoute(GetByIdName, new { id = movieDto.Id }, movieDto);
    }
}