using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Entities;

namespace MoviesAPI.Controllers;

[Route("api/genres")]
public class GenresController
{
    [HttpGet]
    public List<Genre> Get()
    {
        var repository = new InMemoryRepository();
        var genres = repository.GetAllGenres();
        return genres;
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