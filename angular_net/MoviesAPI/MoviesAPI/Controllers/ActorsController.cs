using CoreBusiness;
using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MoviesAPI.Utilities;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

[Route("api/actors")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isadmin")]
public class ActorsController: CustomBaseController
{
    private readonly IActorsRepository _actorsRepository;
    private const string CacheTag = "actors";
    private const string GetByIdName = "GetActorById";

    public ActorsController(IActorsRepository actorsRepository, IOutputCacheStore outputCacheStore)
        :base(outputCacheStore)
    {
        _actorsRepository = actorsRepository;
    }

    [HttpGet("all")]
    [OutputCache(Tags = [CacheTag])]
    public async Task<List<ActorDto>> Get()
    {
        return await _actorsRepository.Get();
    }

    [HttpGet]
    [OutputCache(Tags = [CacheTag])]
    public async Task<List<ActorDto>> Get([FromQuery] PaginationDto paginationDto)
    {
        var count = await _actorsRepository.Count();
        HttpContext.InsertPaginationParametersInHeader(count);
        return await _actorsRepository.Get(paginationDto);
    }

    [HttpGet("{name}")]
    public async Task<List<MovieActorDto>> Get(string name)
    {
        return await _actorsRepository.Get(name);
    }
    
    [HttpGet("{id:int}", Name = GetByIdName)]
    [OutputCache(Tags = [CacheTag])]
    public async Task<ActionResult<ActorDto>> Get(int id)
    {
        var actorDto = await _actorsRepository.Get(id);

        return Get(actorDto);
    }

    [HttpPost]
    public async Task<CreatedAtRouteResult> Post([FromForm] ActorCreationDto actorCreationDto)
    {
        var actorDto = await _actorsRepository.Add(actorCreationDto);
        return await Post(actorDto, CacheTag, GetByIdName);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromForm] ActorCreationDto actorCreationDto)
    {
        var found = await _actorsRepository.Update(id, actorCreationDto);

        return await PutDelete(found, CacheTag);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var found = await _actorsRepository.Delete(id);

        return await PutDelete(found, CacheTag);
    }
}