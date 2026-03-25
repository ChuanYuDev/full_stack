using System.ComponentModel.DataAnnotations;

namespace CoreBusiness.DTOs;

public class MovieActorCreationDto
{
    public int ActorId { get; set; }
    
    [StringLength(300)]
    public required string Character { get; set; }
}