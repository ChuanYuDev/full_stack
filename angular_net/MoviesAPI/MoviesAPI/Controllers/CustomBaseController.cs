using CoreBusiness.DTOs;
using CoreBusiness.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MoviesAPI.Utilities;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

public class CustomBaseController: ControllerBase
{
    private readonly IOutputCacheStore _outputCacheStore;

    public CustomBaseController(IOutputCacheStore outputCacheStore)
    {
        _outputCacheStore = outputCacheStore;
    }

    protected ActionResult<TDto> Get<TDto>(TDto? dto)
    {
        if (dto is null)
        {
            return NotFound();
        }

        return dto;
    }

    protected async Task<CreatedAtRouteResult> Post<TDto>(TDto dto, string cacheTag, string routeName)
        where TDto: IId
    {
        await _outputCacheStore.EvictByTagAsync(cacheTag, default);
        return CreatedAtRoute(routeName, new { id = dto.Id }, dto);
    }

    protected async Task<IActionResult> PutDelete(bool found, string cacheTag)
    {
        if (!found)
        {
            return NotFound();
        }

        await _outputCacheStore.EvictByTagAsync(cacheTag, default);

        return NoContent();
    }
}