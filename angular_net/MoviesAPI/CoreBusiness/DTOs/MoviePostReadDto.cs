namespace CoreBusiness.DTOs;

public class MoviePostReadDto
{
    public List<GenreDto> Genres { get; set; } = new List<GenreDto>();
    public List<TheaterDto> Theaters { get; set; } = new List<TheaterDto>();
}