namespace CoreBusiness.DTOs;

public class MovieActorDto
{
    // ActorId
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Picture { get; set; }
    public string? Character { get; set; }
}