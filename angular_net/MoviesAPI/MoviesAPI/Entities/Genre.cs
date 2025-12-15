using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.Entities;

public class Genre
{
    public int Id { get; set; }    
    
    [Required(ErrorMessage = "You must fill the {0} field")]
    [StringLength(maximumLength: 10)]
    public required string Name { get; set; }
    
    [Range(minimum: 18, maximum: 120)]
    public int Age { get; set; }
    
    [CreditCard]
    public string? CreditCard { get; set; }
    
    [Url]
    public string? WebPage { get; set; }
}