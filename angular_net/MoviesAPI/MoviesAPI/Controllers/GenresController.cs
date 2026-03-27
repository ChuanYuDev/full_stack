using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

[Route("api/genres")]
[ApiController]
public class GenresController: CustomBaseController<GenreCreationDto, GenreDto>
{
    private const string CacheTag = "genres";
    private const string GetByIdName = "GetGenreById";

    public GenresController(IGenresRepository genresRepository, IOutputCacheStore outputCacheStore)
        : base(genresRepository, outputCacheStore, CacheTag)
    {
    }
    
    [HttpGet("all-genres")]
    [OutputCache(Tags = [CacheTag])]
    public async Task<List<GenreDto>> GetAll()
    {
        return await GetAllEntities();
    }

    [HttpGet]
    [OutputCache(Tags = [CacheTag])]
    public async Task<List<GenreDto>> Get([FromQuery] PaginationDto paginationDto)
    {
        return await GetEntities(paginationDto);
    }

    [HttpGet("{id:int}", Name = GetByIdName)]
    [OutputCache(Tags = [CacheTag])]
    public async Task<ActionResult<GenreDto>> Get(int id)
    {
        return await GetEntityById(id);
    }

    [HttpPost]
    public async Task<CreatedAtRouteResult> Post([FromBody] GenreCreationDto genreCreationDto)
    {
        return await PostEntity(genreCreationDto, GetByIdName);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] GenreCreationDto genreCreationDto)
    {
        return await PutEntity(id, genreCreationDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await DeleteEntity(id);
    }
}