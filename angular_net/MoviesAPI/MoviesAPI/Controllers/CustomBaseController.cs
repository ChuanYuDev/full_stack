using CoreBusiness.DTOs;
using CoreBusiness.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MoviesAPI.Utilities;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

public class CustomBaseController<TCreationDto, TDto>: ControllerBase
    where TDto: IId
{
    private readonly IRepository<TCreationDto, TDto> _entitiesRepository;
    private readonly IOutputCacheStore _outputCacheStore;
    private readonly string _cacheTag;

    public CustomBaseController(IRepository<TCreationDto, TDto> entitiesRepository, IOutputCacheStore outputCacheStore, string cacheTag)
    {
        _entitiesRepository = entitiesRepository;
        _outputCacheStore = outputCacheStore;
        _cacheTag = cacheTag;
    }

    protected async Task<List<TDto>> GetEntities()
    {
        return await _entitiesRepository.Get();
    }

    protected async Task<List<TDto>> GetEntities(PaginationDto paginationDto)
    {
        var count = await _entitiesRepository.Count();
        HttpContext.InsertPaginationParametersInHeader(count);
        return await _entitiesRepository.Get(paginationDto);
    }

    protected async Task<ActionResult<TDto>> GetEntity(int id)
    {
        var dto = await _entitiesRepository.Get(id);

        if (dto is null)
        {
            return NotFound();
        }

        return dto;
    }

    protected async Task<CreatedAtRouteResult> PostEntity(TCreationDto creationDto, string routeName)
    {
        var dto = await _entitiesRepository.Add(creationDto);
        await _outputCacheStore.EvictByTagAsync(_cacheTag, default);
        return CreatedAtRoute(routeName, new { id = dto.Id }, dto);
    }

    protected async Task<IActionResult> PutEntity(int id, TCreationDto creationDto)
    {
        var found = await _entitiesRepository.Update(id, creationDto);

        if (!found)
        {
            return NotFound();
        }

        await _outputCacheStore.EvictByTagAsync(_cacheTag, default);

        return NoContent();
    }

    protected async Task<IActionResult> DeleteEntity(int id)
    {
        var found = await _entitiesRepository.Delete(id);

        if (!found)
        {
            return NotFound();
        }

        await _outputCacheStore.EvictByTagAsync(_cacheTag, default);

        return NoContent();
    }
}