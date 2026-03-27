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
        ConfigureMovies();
    }

    private void ConfigureGenres()
    {
        CreateMap<GenreCreationDto, Genre>();
        CreateMap<Genre, GenreDto>();
    }

    private void ConfigureActors()
    {
        CreateMap<ActorCreationDto, Actor>().ForMember(actor => actor.Picture, config =>
        {
            config.Ignore();
        });

        CreateMap<Actor, ActorDto>();
        CreateMap<Actor, MovieActorDto>();
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

    private void ConfigureMovies()
    {
        CreateMap<MovieCreationDto, Movie>()
            .ForMember(movie => movie.Poster, config =>
            {
                config.Ignore();
            })
            .ForMember(movie => movie.MoviesGenres, config =>
            {
                config.MapFrom(movieCreationDto => movieCreationDto.GenresIds.Select(id => new MovieGenre { GenreId = id }));
            })
            .ForMember(movie => movie.MoviesTheaters, config =>
            {
                config.MapFrom(movieCreationDto => movieCreationDto.TheatersIds.Select(id => new MovieTheater { TheaterId = id }));
            })
            .ForMember(movie => movie.MoviesActors, config =>
            {
                config.MapFrom(movieCreationDto => movieCreationDto.Actors.Select(movieActorCreationDto => new MovieActor
                {
                    ActorId = movieActorCreationDto.Id,
                    Character = movieActorCreationDto.Character
                }));
            });
        CreateMap<Movie, MovieDto>();
    }
}