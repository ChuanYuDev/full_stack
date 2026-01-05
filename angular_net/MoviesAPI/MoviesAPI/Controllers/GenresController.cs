using CoreBusiness;
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
    private const string CacheTag = "genres";

    public GenresController(IGenresRepository genresRepository, IOutputCacheStore outputCacheStore)
    {
        _genresRepository = genresRepository;
        _outputCacheStore = outputCacheStore;
    }

    [HttpGet]
    [HttpGet("all-genres")]
    [HttpGet("/all-of-the-genres")]
    [OutputCache(Tags = [CacheTag])]
    public List<Genre> Get()
    {
        var genres = _genresRepository.GetAllGenres();
        return genres;
    }

    [HttpGet("{id:int}")]
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

    [HttpGet("{name}")]
    public async Task<ActionResult<Genre>> Get(string name, [FromQuery] int id)
    {
        return new Genre { Id = id, Name = name };
    }

    [HttpPost]
    public async Task<ActionResult<Genre>> Post([FromBody] Genre genre)
    {
        var genreWithSameNameExists = _genresRepository.Exists(genre.Name);

        if (genreWithSameNameExists)
        {
            return BadRequest($"There's already a genre with the name {genre.Name}");
        }

        _genresRepository.Create(genre);
        await _outputCacheStore.EvictByTagAsync(CacheTag, default);
        return genre;
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