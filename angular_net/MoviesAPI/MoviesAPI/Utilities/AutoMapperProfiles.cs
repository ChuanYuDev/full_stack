using AutoMapper;
using CoreBusiness;
using CoreBusiness.DTOs;

namespace MoviesAPI.Utilities;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        ConfigureGenres();
    }

    private void ConfigureGenres()
    {
        CreateMap<GenreCreationDto, Genre>();
        CreateMap<Genre, GenreDto>();
    }
}