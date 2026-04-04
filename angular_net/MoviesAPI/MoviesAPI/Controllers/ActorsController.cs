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
    private readonly IActorsRepository _actorsRepository;
    private const string CacheTag = "actors";
    private const string GetByIdName = "GetActorById";

    public ActorsController(IActorsRepository actorsRepository, IOutputCacheStore outputCacheStore)
        : base(actorsRepository, outputCacheStore, CacheTag)
    {
        _actorsRepository = actorsRepository;
    }

    [HttpGet("all-actors")]
    [OutputCache(Tags = [CacheTag])]
    public async Task<List<ActorDto>> Get()
    {
        return await GetEntities();
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
        return await GetEntity(id);
    }

    [HttpGet("{name}")]
    public async Task<List<MovieActorDto>> Get(string name)
    {
        return await _actorsRepository.Get(name);
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