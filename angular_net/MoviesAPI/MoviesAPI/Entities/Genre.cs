using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Entities;

public class Genre
{
    public int Id { get; set; }    
    
    [Required(ErrorMessage = "You must fill the {0} field")]
    public required string Name { get; set; }
}