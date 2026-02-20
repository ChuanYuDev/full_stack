using AutoMapper;
using CoreBusiness;
using CoreBusiness.DTOs;

namespace MoviesAPI.Utilities;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        ConfigureGenres();
        ConfigureActors();
    }

    private void ConfigureGenres()
    {
        CreateMap<GenreCreationDto, Genre>();
        CreateMap<Genre, GenreDto>();
    }

    private void ConfigureActors()
    {
        CreateMap<ActorCreationDto, Actor>().ForMember(actor => actor.Picture, options =>
        {
            options.Ignore();
        });
    }
}