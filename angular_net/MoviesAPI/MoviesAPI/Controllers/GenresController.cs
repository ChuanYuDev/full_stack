using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MoviesAPI.Entities;

namespace MoviesAPI.Controllers;

[Route("api/genres")]
[ApiController]
public class GenresController: ControllerBase
{
    private readonly IRepository repository;
    private readonly IOutputCacheStore outputCacheStore;
    private const string cacheTag = "genres";

    public GenresController(IRepository repository, IOutputCacheStore outputCacheStore)
    {
        this.repository = repository;
        this.outputCacheStore = outputCacheStore;
    }

    [HttpGet]
    [HttpGet("all-genres")]
    [HttpGet("/all-of-the-genres")]
    [OutputCache(Tags = [cacheTag])]
    public List<Genre> Get()
    {
        var genres = repository.GetAllGenres();
        return genres;
    }

    [HttpGet("{id:int}")]
    [OutputCache(Tags = [cacheTag])]
    public async Task<ActionResult<Genre>> Get(int id)
    {
        var genre = await repository.GetById(id);

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
        var genreWithSameNameExists = repository.Exists(genre.Name);

        if (genreWithSameNameExists)
        {
            return BadRequest($"There's already a genre with the name {genre.Name}");
        }

        repository.Create(genre);
        await outputCacheStore.EvictByTagAsync(cacheTag, default);
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