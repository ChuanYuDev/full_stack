using System.ComponentModel.DataAnnotations;

namespace CoreBusiness.DTOs;

public class MovieRatingCreationDto
{
    [Range(1, 5)]
    public int Rate { get; set; }
    
    public int MovieId { get; set; }
}