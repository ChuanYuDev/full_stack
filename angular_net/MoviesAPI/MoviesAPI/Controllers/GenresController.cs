using CoreBusiness;
using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MoviesAPI.Utilities;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

[Route("api/genres")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isadmin")]
public class GenresController: CustomBaseController
{
    private readonly IGenresRepository _genresRepository;
    private const string CacheTag = "genres";
    private const string GetByIdName = "GetGenreById";

    public GenresController(IGenresRepository genresRepository, IOutputCacheStore outputCacheStore)
        : base(outputCacheStore)
    {
        _genresRepository = genresRepository;
    }
    
    [HttpGet("all")]
    [AllowAnonymous]
    [OutputCache(Tags = [CacheTag])]
    public async Task<List<GenreDto>> Get()
    {
        return await _genresRepository.Get();
    }

    [HttpGet]
    [OutputCache(Tags = [CacheTag])]
    public async Task<List<GenreDto>> Get([FromQuery] PaginationDto paginationDto)
    {
        var count = await _genresRepository.Count();
        HttpContext.InsertPaginationParametersInHeader(count);
        return await _genresRepository.Get(paginationDto);
    }

    [HttpGet("{id:int}", Name = GetByIdName)]
    [OutputCache(Tags = [CacheTag])]
    public async Task<ActionResult<GenreDto>> Get(int id)
    {
        var genreDto = await _genresRepository.Get(id);

        return Get(genreDto);
    }

    [HttpPost]
    public async Task<CreatedAtRouteResult> Post([FromBody] GenreCreationDto genreCreationDto)
    {
        var genreDto = await _genresRepository.Add(genreCreationDto);

        return await Post(genreDto, CacheTag, GetByIdName);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, [FromBody] GenreCreationDto genreCreationDto)
    {
        var found = await _genresRepository.Update(id, genreCreationDto);

        return await PutDelete(found, CacheTag);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var found = await _genresRepository.Delete(id);

        return await PutDelete(found, CacheTag);
    }
}