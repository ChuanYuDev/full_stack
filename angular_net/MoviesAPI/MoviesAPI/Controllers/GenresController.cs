using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Entities;

namespace MoviesAPI.Controllers;

[Route("api/genres")]
public class GenresController: ControllerBase
{
    [HttpGet("api/genres")]
    [HttpGet]
    [HttpGet("/all-of-the-genres")]
    public List<Genre> Get()
    {
        var repository = new InMemoryRepository();
        var genres = repository.GetAllGenres();
        return genres;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Genre> Get(int id)
    {
        var repository = new InMemoryRepository();
        var genre = repository.GetById(id);

        if (genre is null)
        {
            return NotFound();
        }
        
        return genre;
    }

    [HttpPost]
    public void Post()
    {
        
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