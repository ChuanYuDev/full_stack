using System.ComponentModel.DataAnnotations;

namespace CoreBusiness.DTOs;

public class MovieRatingCreationDto
{
    public int MovieId { get; set; }
    
    [Range(1, 5)]
    public int Rate { get; set; }
}