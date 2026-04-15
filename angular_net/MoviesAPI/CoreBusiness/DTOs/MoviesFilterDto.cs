namespace CoreBusiness.DTOs;

public class MoviesFilterDto
{
    public int Page { get; set; }
    public int RecordsPerPage { get; set; }

    public PaginationDto PaginationDto
    {
        get
        {
            return new PaginationDto { Page = Page, RecordsPerPage = RecordsPerPage };
        }
    }

    public string? Title { get; set; }
    public int? GenreId { get; set; }
    public bool? UpcomingReleases { get; set; }
    public bool? InTheaters { get; set; }
}