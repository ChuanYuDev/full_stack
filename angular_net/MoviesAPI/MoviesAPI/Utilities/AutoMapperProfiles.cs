using AutoMapper;
using CoreBusiness;
using CoreBusiness.DTOs;
using NetTopologySuite.Geometries;

namespace MoviesAPI.Utilities;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles(GeometryFactory geometryFactory)
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

        CreateMap<Actor, ActorDto>();
    }

    // private void ConfigureTheaters()
    // {
    //     CreateMap<Theater, Actor>()
    //         .ForMember(x => x.Picture, x => x.MapFrom(p => p.Location.Y));
    // }
}