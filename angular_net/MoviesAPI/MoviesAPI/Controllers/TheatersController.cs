using CoreBusiness;
using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MoviesAPI.Utilities;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

[Route("api/theaters")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isadmin")]
public class TheatersController: CustomBaseController
{
    private readonly ITheatersRepository _theatersRepository;
    private const string CacheTag = "theaters";
    private const string GetByIdName = "GetTheaterById";

    public TheatersController(ITheatersRepository theatersRepository, IOutputCacheStore outputCacheStore)
        : base(outputCacheStore)
    {
        _theatersRepository = theatersRepository;
    }

    [HttpGet("all")]
    [OutputCache(Tags = [CacheTag])]
    public async Task<List<TheaterDto>> Get()
    {
        return await _theatersRepository.Get();
    }
    
    [HttpGet]
    [OutputCache(Tags = [CacheTag])]
    public async Task<List<TheaterDto>> Get([FromQuery] PaginationDto paginationDto)
    {
        var count = await _theatersRepository.Count();
        HttpContext.InsertPaginationParametersInHeader(count);
        return await _theatersRepository.Get(paginationDto);
    }

    [HttpGet("{id:int}", Name = GetByIdName)]
    [OutputCache(Tags = [CacheTag])]
    public async Task<ActionResult<TheaterDto>> Get(int id)
    {
        var theaterDto = await _theatersRepository.Get(id);

        return Get(theaterDto);
    }

    [HttpPost]
    public async Task<CreatedAtRouteResult> Post([FromBody] TheaterCreationDto theaterCreationDto)
    {
        var theaterDto = await _theatersRepository.Add(theaterCreationDto);
        return await Post(theaterDto, CacheTag, GetByIdName);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] TheaterCreationDto theaterCreationDto)
    {
        var found = await _theatersRepository.Update(id, theaterCreationDto);

        return await PutDelete(found, CacheTag);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var found = await _theatersRepository.Delete(id);

        return await PutDelete(found, CacheTag);
    }
}