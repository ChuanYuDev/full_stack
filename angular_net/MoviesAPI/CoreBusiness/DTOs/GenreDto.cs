using CoreBusiness.Interfaces;

namespace CoreBusiness.DTOs;

public class GenreDto: IId
{
    public int Id { get; set; }    
    public required string Name { get; set; }
}