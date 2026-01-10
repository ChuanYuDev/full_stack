using AutoMapper;
using CoreBusiness;
using CoreBusiness.DTOs;

namespace MoviesAPI.Utilities;

public class AutoMapperProfile: Profile
{
    public AutoMapperProfile()
    {
        ConfigureGenres();
    }

    private void ConfigureGenres()
    {
        CreateMap<GenreCreationDto, Genre>();
        CreateMap<Genre, GenreDto>();
    }
}