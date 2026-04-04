namespace CoreBusiness.DTOs;

public class LandingDto
{
    public List<MovieDto> InTheaters { get; set; } = [];
    public List<MovieDto> UpcomingReleases { get; set; } = [];
}