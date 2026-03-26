namespace CoreBusiness.DTOs;

public class MoviePostGetDto
{
    public List<GenreDto> Genres { get; set; } = new List<GenreDto>();
    public List<TheaterDto> Theaters { get; set; } = new List<TheaterDto>();
}