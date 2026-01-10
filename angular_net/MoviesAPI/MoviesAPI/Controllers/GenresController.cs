using AutoMapper;
using CoreBusiness;
using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

[Route("api/genres")]
[ApiController]
public class GenresController: ControllerBase
{
    private readonly IGenresRepository _genresRepository;
    private readonly IOutputCacheStore _outputCacheStore;
    private readonly IMapper _mapper;
    private const string CacheTag = "genres";

    public GenresController(IGenresRepository genresRepository, IOutputCacheStore outputCacheStore, IMapper mapper)
    {
        _genresRepository = genresRepository;
        _outputCacheStore = outputCacheStore;
        _mapper = mapper;
    }

    [HttpGet]
    [OutputCache(Tags = [CacheTag])]
    public List<Genre> Get()
    {
        var genres = _genresRepository.GetAllGenres();
        return genres;
    }

    [HttpGet("{id:int}", Name = "GetGenreById")]
    [OutputCache(Tags = [CacheTag])]
    public async Task<ActionResult<Genre>> Get(int id)
    {
        var genre = await _genresRepository.GetById(id);

        if (genre is null)
        {
            return NotFound();
        }
        
        return genre;
    }

    [HttpPost]
    public async Task<CreatedAtRouteResult> Post([FromBody] GenreCreationDto genreCreationDto)
    {
        var genre = _mapper.Map<Genre>(genreCreationDto);
        
        await _genresRepository.Add(genre);
        await _outputCacheStore.EvictByTagAsync(CacheTag, CancellationToken.None);

        var genreDto = _mapper.Map<GenreDto>(genre);
        return CreatedAtRoute("GetGenreById", new {id = genreDto.Id}, genreDto);
    }

    [HttpPut]
    public void Put()
    {
        
    }

    [HttpDelete]
    public void Delete()
    {
        
    }
}