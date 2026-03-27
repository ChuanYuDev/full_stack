using System.ComponentModel.DataAnnotations;

namespace CoreBusiness.DTOs;

public class TheaterCreationDto
{
    [Required]
    [StringLength(75)]
    public required string Name { get; set; }
    
    [Range(-90, 90)]
    public required double Latitude { get; set; }
    
    [Range(-180, 180)]
    public required double Longitude { get; set; }
}