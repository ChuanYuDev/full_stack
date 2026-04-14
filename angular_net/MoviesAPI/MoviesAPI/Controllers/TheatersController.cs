using CoreBusiness;
using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

[Route("api/theaters")]
[ApiController]
public class TheatersController: Controller<Theater, TheaterCreationDto, TheaterDto>
{
    private const string CacheTag = "theaters";
    private const string GetByIdName = "GetTheaterById";

    public TheatersController(ITheatersRepository theatersRepository, IOutputCacheStore outputCacheStore)
        : base(theatersRepository, outputCacheStore, CacheTag)
    {
    }

    [HttpGet("all")]
    [OutputCache(Tags = [CacheTag])]
    public async Task<List<TheaterDto>> Get()
    {
        return await GetEntities();
    }
    
    [HttpGet]
    [OutputCache(Tags = [CacheTag])]
    public async Task<List<TheaterDto>> Get([FromQuery] PaginationDto paginationDto)
    {
        return await GetEntities(paginationDto);
    }

    [HttpGet("{id:int}", Name = GetByIdName)]
    [OutputCache(Tags = [CacheTag])]
    public async Task<ActionResult<TheaterDto>> Get(int id)
    {
        return await GetEntity(id);
    }

    [HttpPost]
    public async Task<CreatedAtRouteResult> Post([FromBody] TheaterCreationDto theaterCreationDto)
    {
        return await PostEntity(theaterCreationDto, GetByIdName);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] TheaterCreationDto theaterCreationDto)
    {
        return await PutEntity(id, theaterCreationDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await DeleteEntity(id);
    }
}