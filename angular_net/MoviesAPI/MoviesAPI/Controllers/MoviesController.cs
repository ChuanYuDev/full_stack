using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

[Route("api/movies")]
[ApiController]
public class MoviesController: CustomBaseController<MovieCreationDto, MovieDto>
{
    private readonly IRepository<GenreCreationDto, GenreDto> _genresRepository;
    private readonly IRepository<TheaterCreationDto, TheaterDto> _theatersRepository;
    private const string CacheTag = "movies";
    private const string GetByIdName = "GetMovieById";

    public MoviesController(
        IRepository<GenreCreationDto, GenreDto> genresRepository,     
        IRepository<TheaterCreationDto, TheaterDto> theatersRepository,     
        IRepository<MovieCreationDto, MovieDto> moviesRepository, 
        IOutputCacheStore outputCacheStore
    ): base(moviesRepository, outputCacheStore, CacheTag)
    {
        _genresRepository = genresRepository;
        _theatersRepository = theatersRepository;
    }

    [HttpGet("post-read")]
    public async Task<ActionResult<MoviePostReadDto>> PostRead()
    {
        var genres = await _genresRepository.GetAll();
        var theaters = await _theatersRepository.GetAll();

        return new MoviePostReadDto { Genres = genres, Theaters = theaters };
    }

    [HttpPost]
    public async Task<CreatedAtRouteResult> Post([FromForm] MovieCreationDto movieCreationDto)
    {
        return await PostEntity(movieCreationDto, GetByIdName);
    }
}