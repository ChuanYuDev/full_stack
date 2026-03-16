using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

[Route("api/theaters")]
[ApiController]
public class TheatersController: CustomBaseController<TheaterCreationDto, TheaterDto>
{
    private const string CacheTag = "theaters";
    private const string GetByIdName = "GetTheaterById";

    public TheatersController(IRepository<TheaterCreationDto, TheaterDto> theatersRepository, IOutputCacheStore outputCacheStore)
        : base(theatersRepository, outputCacheStore, CacheTag)
    {
    }

    [HttpGet("all-theaters")]
    [OutputCache(Tags = [CacheTag])]
    public async Task<List<TheaterDto>> GetAll()
    {
        return await GetAllEntities();
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
        return await GetEntityById(id);
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