namespace CoreBusiness.DTOs;

public class MovieDetailsDto: MovieDto
{
    public List<GenreDto> Genres { get; set; } = [];

    public List<TheaterDto> Theaters { get; set; } = [];
    
    public List<MovieActorDto> Actors { get; set; } = [];
}