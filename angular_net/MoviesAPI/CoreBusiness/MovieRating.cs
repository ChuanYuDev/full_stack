using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CoreBusiness;

public class MovieRating
{
    public int Id { get; set; }
    
    [Range(1, 5)]
    public int Rate { get; set; }
    
    public int MovieId { get; set; }
    public required string UserId { get; set; }

    public Movie Movie { get; set; } = null!;
    public IdentityUser User { get; set; } = null!;
}