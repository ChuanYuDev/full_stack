using CoreBusiness.Interfaces;

namespace CoreBusiness.DTOs;

public class MovieDto: IId
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? Trailer { get; set; }
    public string? Poster { get; set; }
}