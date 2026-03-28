using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

[Route("api/movies")]
[ApiController]
public class MoviesController: CustomBaseController<MovieCreationDto, MovieDto>
{
    private readonly IGenresRepository _genresRepository;
    private readonly ITheatersRepository _theatersRepository;
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
    }

    [HttpGet("post-get")]
    public async Task<ActionResult<MoviePostGetDto>> PostRead()
    {
        var genres = await _genresRepository.GetAll();
        var theaters = await _theatersRepository.GetAll();

        return new MoviePostGetDto { Genres = genres, Theaters = theaters };
    }

    [HttpGet("{id:int}", Name = GetByIdName)]
    [OutputCache(Tags = [CacheTag])]
    public Task<ActionResult<MovieDto>> Get(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<CreatedAtRouteResult> Post([FromForm] MovieCreationDto movieCreationDto)
    {
        return await PostEntity(movieCreationDto, GetByIdName);
    }
}