using AutoMapper;
using CoreBusiness;
using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MoviesAPI.Utilities;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

[Route("api/genres")]
[ApiController]
public class GenresController: ControllerBase
{
    private readonly IGenresRepository _genresRepository;
    private readonly IOutputCacheStore _outputCacheStore;
    private const string CacheTag = "genres";

    public GenresController(IGenresRepository genresRepository, IOutputCacheStore outputCacheStore)
    {
        _genresRepository = genresRepository;
        _outputCacheStore = outputCacheStore;
    }
    
    [HttpGet("all-genres")]
    [OutputCache(Tags = [CacheTag])]
    public async Task<List<GenreDto>> GetAll()
    {
        return await _genresRepository.GetAll();
    }

    [HttpGet]
    [OutputCache(Tags = [CacheTag])]
    public async Task<List<GenreDto>> Get([FromQuery] PaginationDto paginationDto)
    {
        var count = await _genresRepository.Count();
        HttpContext.InsertPaginationParametersInHeader(count);
        return await _genresRepository.Get(paginationDto);
    }

    [HttpGet("{id:int}", Name = "GetGenreById")]
    [OutputCache(Tags = [CacheTag])]
    public async Task<ActionResult<GenreDto>> Get(int id)
    {
        var genre = await _genresRepository.GetById(id);

        if (genre is null)
        {
            return NotFound();
        }
        
        return genre;
    }

    [HttpPost]
    public async Task<CreatedAtRouteResult> Post([FromBody] GenreCreationDto genreCreationDto)
    {
        var genreDto = await _genresRepository.Add(genreCreationDto);
        await _outputCacheStore.EvictByTagAsync(CacheTag, default);

        return CreatedAtRoute("GetGenreById", new {id = genreDto.Id}, genreDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] GenreCreationDto genreCreationDto)
    {
        var genreExists = await _genresRepository.Exists(id);

        if (!genreExists)
        {
            return NotFound();
        }

        await _genresRepository.Update(id, genreCreationDto);
        await _outputCacheStore.EvictByTagAsync(CacheTag, default);

        return NoContent();
    }

    [HttpDelete]
    public void Delete()
    {
        
    }
}