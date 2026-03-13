using CoreBusiness.Interfaces;

namespace CoreBusiness.DTOs;

public class ActorDto: IId
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Picture { get; set; }
}