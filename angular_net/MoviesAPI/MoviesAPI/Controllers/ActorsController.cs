using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
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

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] ActorCreationDto actorCreationDto)
    {
        await _actorsRepository.Add(actorCreationDto);
        await _outputCacheStore.EvictByTagAsync(CacheTag, default);

        return Ok();
    }
}