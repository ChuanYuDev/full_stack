using CoreBusiness;
using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

[Route("api/movies")]
[ApiController]
public class MoviesController: BaseController<Movie, MovieCreationDto, MovieDto, MovieDetailsDto>
{
    private readonly IGenresRepository _genresRepository;
    private readonly ITheatersRepository _theatersRepository;
    private readonly IMoviesRepository _moviesRepository;
    private const string CacheTag = "movies";
    private const string GetByIdName = "GetMovieById";

    public MoviesController(
        IGenresRepository genresRepository,
        ITheatersRepository theatersRepository,
        IMoviesRepository moviesRepository,
        IOutputCacheStore outputCacheStore
    ): base(moviesRepository, outputCacheStore, CacheTag)
    {
        _genresRepository = genresRepository;
        _theatersRepository = theatersRepository;
        _moviesRepository = moviesRepository;
    }

    [HttpGet("landing")]
    [OutputCache(Tags = [CacheTag])]
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
        return await GetEntity(id);
    }

    [HttpGet("post-get")]
    public async Task<ActionResult<MoviePostGetDto>> PostGet()
    {
        var genres = await _genresRepository.Get();
        var theaters = await _theatersRepository.Get();

        return new MoviePostGetDto { Genres = genres, Theaters = theaters };
    }

    [HttpGet("put-get/{id:int}")]
    public async Task<ActionResult<MoviePutGetDto>> PutGet(int id)
    {
        var movieDetailsDto = await _moviesRepository.Get(id);

        if (movieDetailsDto is null)
        {
            return NotFound();
        }

        var genresIds = movieDetailsDto.Genres.Select(g => g.Id);

        var nonSelectedGenres = await _genresRepository.Get(where: g => !genresIds.Contains(g.Id));

        var theatersIds = movieDetailsDto.Theaters.Select(t => t.Id);

        var nonSelectedTheaters = await _theatersRepository.Get(where: t => !theatersIds.Contains(t.Id));

        return new MoviePutGetDto
        {
            Movie = movieDetailsDto,
            SelectedGenres = movieDetailsDto.Genres,
            NonSelectedGenres = nonSelectedGenres,
            SelectedTheaters = movieDetailsDto.Theaters,
            NonSelectedTheaters = nonSelectedTheaters,
            Actors = movieDetailsDto.Actors
        };
    }

    [HttpPost]
    public async Task<CreatedAtRouteResult> Post([FromForm] MovieCreationDto movieCreationDto)
    {
        return await PostEntity(movieCreationDto, GetByIdName);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromForm] MovieCreationDto movieCreationDto)
    {
        return await PutEntity(id, movieCreationDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await DeleteEntity(id);
    }
}