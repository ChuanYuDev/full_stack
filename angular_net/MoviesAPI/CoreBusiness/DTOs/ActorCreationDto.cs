using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CoreBusiness.DTOs;

public class ActorCreationDto
{
    [Required]
    [StringLength(150)]
    public required string Name { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public IFormFile? Picture { get; set; }

}