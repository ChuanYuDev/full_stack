using CoreBusiness;
using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MoviesAPI.Utilities;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

[Route("api/movies")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isadmin")]
public class MoviesController: CustomBaseController
{
    private readonly IGenresRepository _genresRepository;
    private readonly ITheatersRepository _theatersRepository;
    private readonly IMoviesRepository _moviesRepository;
    private readonly IRatingsSqlRepository _ratingsSqlRepository;
    private const string CacheTag = "movies";
    private const string GetByIdName = "GetMovieById";

    public MoviesController(
        IGenresRepository genresRepository,
        ITheatersRepository theatersRepository,
        IMoviesRepository moviesRepository,
        IRatingsSqlRepository ratingsSqlRepository,
        IOutputCacheStore outputCacheStore
    ): base(outputCacheStore)
    {
        _genresRepository = genresRepository;
        _theatersRepository = theatersRepository;
        _moviesRepository = moviesRepository;
        _ratingsSqlRepository = ratingsSqlRepository;
    }

    [HttpGet("landing")]
    [OutputCache(Tags = [CacheTag])]
    [AllowAnonymous]
    public async Task<ActionResult<LandingDto>> Get()
    {
        var today = DateTime.Today;
        var top = 6;

        var inTheaters = await _moviesRepository.Get(where: m => m.MoviesTheaters.Count > 0, top);
        var upcomingReleases = await _moviesRepository.Get(where: m => m.ReleaseDate > today, top);

        return new LandingDto { InTheaters = inTheaters, UpcomingReleases = upcomingReleases };
    }

    [HttpGet("filter")]
    [AllowAnonymous]
    public async Task<ActionResult<List<MovieDto>>> Filter([FromQuery] MoviesFilterDto moviesFilterDto)
    {
        var result = await _moviesRepository.Filter(moviesFilterDto);
        HttpContext.InsertPaginationParametersInHeader(result.MoviesNum);
        return result.Movies;
    }

    [HttpGet("{id:int}", Name = GetByIdName)]
    [OutputCache(Tags = [CacheTag])]
    [AllowAnonymous]
    public async Task<ActionResult<MovieDetailsDto>> Get(int id)
    {
        var movieDetailsDto = await _moviesRepository.Get(id);

        if (movieDetailsDto is null)
        {
            return NotFound();
        }

        var identity = HttpContext.User.Identity;

        if (identity is null)
        {
            throw new InvalidOperationException("User identity is null");
        }

        if (identity.IsAuthenticated)
        {
            movieDetailsDto.UserVote = await _ratingsSqlRepository.GetRate(id);
        }

        return movieDetailsDto;
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
        var movieDto = await _moviesRepository.Add(movieCreationDto);

        return await Post(movieDto, CacheTag, GetByIdName);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromForm] MovieCreationDto movieCreationDto)
    {
        var found = await _moviesRepository.Update(id, movieCreationDto);

        return await PutDelete(found, CacheTag);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var found = await _moviesRepository.Delete(id);

        return await PutDelete(found, CacheTag);
    }
}