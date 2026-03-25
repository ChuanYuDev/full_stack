using System.ComponentModel.DataAnnotations;
using CoreBusiness.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreBusiness.DTOs;

public class MovieCreationDto
{
    [Required]
    [StringLength(300)]
    public required string Title { get; set; }
    
    public DateTime ReleaseDate { get; set; }
    public string? Trailer { get; set; }
    
    public IFormFile? Poster { get; set; }

    [ModelBinder(BinderType = typeof(ModelBinder))]
    public List<int> GenresIds { get; set; } = new List<int>();
    
    [ModelBinder(BinderType = typeof(ModelBinder))]
    public List<int> TheatersIds { get; set; } = new List<int>();
    
    [ModelBinder(BinderType = typeof(ModelBinder))]
    public List<MovieActorCreationDto> Actors { get; set; } = new List<MovieActorCreationDto>();
}