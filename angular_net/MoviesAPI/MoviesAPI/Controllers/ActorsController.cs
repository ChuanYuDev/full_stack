using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MoviesAPI.Utilities;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

[Route("api/actors")]
[ApiController]
public class ActorsController: CustomBaseController<ActorCreationDto, ActorDto>
{
    private const string CacheTag = "actors";
    private const string GetByIdName = "GetActorById";

    public ActorsController(IRepository<ActorCreationDto, ActorDto> actorsRepository, IOutputCacheStore outputCacheStore)
        : base(actorsRepository, outputCacheStore, CacheTag)
    {
    }

    [HttpGet("all-actors")]
    [OutputCache(Tags = [CacheTag])]
    public async Task<List<ActorDto>> GetAll()
    {
        return await GetAllEntities();
    }

    [HttpGet]
    [OutputCache(Tags = [CacheTag])]
    public async Task<List<ActorDto>> Get([FromQuery] PaginationDto paginationDto)
    {
        return await GetEntities(paginationDto);
    }

    [HttpGet("{id:int}", Name = GetByIdName)]
    [OutputCache(Tags = [CacheTag])]
    public async Task<ActionResult<ActorDto>> Get(int id)
    {
        return await GetEntityById(id);
    }
    
    [HttpPost]
    public async Task<CreatedAtRouteResult> Post([FromForm] ActorCreationDto actorCreationDto)
    {
        return await PostEntity(actorCreationDto, GetByIdName);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromForm] ActorCreationDto actorCreationDto)
    {
        return await PutEntity(id, actorCreationDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await DeleteEntity(id);
    }
}