using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MoviesAPI.Entities;

namespace MoviesAPI.Controllers;

[Route("api/genres")]
[ApiController]
public class GenresController: ControllerBase
{
    private readonly InMemoryRepository repository;

    public GenresController(InMemoryRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    [HttpGet("api/genres")]
    [HttpGet("/all-of-the-genres")]
    public List<Genre> Get()
    {
        var genres = repository.GetAllGenres();
        return genres;
    }

    [HttpGet("{id:int}")]
    [OutputCache]
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
    public ActionResult<Genre> Post([FromBody] Genre genre)
    {
        var genreWithSameNameExists = repository.Exists(genre.Name);

        if (genreWithSameNameExists)
        {
            return BadRequest($"There's already a genre with the name {genre.Name}");
        }
        
        genre.Id = 3;
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