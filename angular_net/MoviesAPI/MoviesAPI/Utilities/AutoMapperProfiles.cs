using AutoMapper;
using CoreBusiness;
using CoreBusiness.DTOs;
using NetTopologySuite.Geometries;

namespace MoviesAPI.Utilities;

public class AutoMapperProfiles: Profile
{
    private readonly GeometryFactory _geometryFactory;

    public AutoMapperProfiles(GeometryFactory geometryFactory)
    {
        _geometryFactory = geometryFactory;
        ConfigureGenres();
        ConfigureActors();
        ConfigureTheaters();
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

    private void ConfigureTheaters()
    {
        CreateMap<TheaterCreationDto, Theater>().ForMember(theater => theater.Location, config =>
        {
            config.MapFrom(theaterCreationDto => _geometryFactory.CreatePoint(new Coordinate(theaterCreationDto.Longitude, theaterCreationDto.Latitude)));
        });

        CreateMap<Theater, TheaterDto>()
            .ForMember(theaterDto => theaterDto.Latitude, config =>
            {
                config.MapFrom(theater => theater.Location.Y);
            })
            .ForMember(theaterDto => theaterDto.Longitude, config =>
            {
                config.MapFrom(theater => theater.Location.X);
            });
    }
}