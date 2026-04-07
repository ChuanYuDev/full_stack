namespace CoreBusiness.DTOs;

public class MoviePutGetDto
{
    public MovieDto Movie { get; set; } = null!;
    public List<GenreDto> SelectedGenres { get; set; } = [];
    public List<GenreDto> NonSelectedGenres { get; set; } = [];
    public List<TheaterDto> SelectedTheaters { get; set; } = [];
    public List<TheaterDto> NonSelectedTheaters { get; set; } = [];
    public List<MovieActorDto> Actors { get; set; } = [];
}