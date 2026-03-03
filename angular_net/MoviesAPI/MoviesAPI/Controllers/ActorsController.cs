using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MoviesAPI.Utilities;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

[Route("api/actors")]
[ApiController]
public class ActorsController: ControllerBase
{
    private readonly IActorsRepository _actorsRepository;
    private readonly IOutputCacheStore _outputCacheStore;
    private const string CacheTag = "actors";

    public ActorsController(IActorsRepository actorsRepository, IOutputCacheStore outputCacheStore)
    {
        _actorsRepository = actorsRepository;
        _outputCacheStore = outputCacheStore;
    }

    [HttpGet]
    [OutputCache(Tags = [CacheTag])]
    public async Task<List<ActorDto>> Get([FromQuery] PaginationDto paginationDto)
    {
        var count = await _actorsRepository.Count();
        HttpContext.InsertPaginationParametersInHeader(count);

        return await _actorsRepository.Get(paginationDto);
    }

    [HttpGet("{id:int}", Name = "GetActorById")]
    [OutputCache(Tags = [CacheTag])]
    public async Task<ActionResult<ActorDto>> Get(int id)
    {
        var actorDto = await _actorsRepository.GetById(id);

        if (actorDto is null)
        {
            return NotFound();
        }

        return actorDto;
    }
    
    [HttpPost]
    public async Task<CreatedAtRouteResult> Post([FromForm] ActorCreationDto actorCreationDto)
    {
        var actorDto = await _actorsRepository.Add(actorCreationDto);
        await _outputCacheStore.EvictByTagAsync(CacheTag, default);

        return CreatedAtRoute("GetActorById", new {id = actorDto.Id}, actorDto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromForm] ActorCreationDto actorCreationDto)
    {
        var found = await _actorsRepository.Update(id, actorCreationDto);

        if (!found)
        {
            return NotFound();
        }

        await _outputCacheStore.EvictByTagAsync(CacheTag, default);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var found = await _actorsRepository.Delete(id);

        if (!found)
        {
            return NotFound();
        }

        await _outputCacheStore.EvictByTagAsync(CacheTag, default);

        return NoContent();
    }
}