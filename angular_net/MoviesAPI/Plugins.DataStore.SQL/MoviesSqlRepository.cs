using AutoMapper;
using CoreBusiness;
using CoreBusiness.DTOs;
using UseCases.DataStoreInterfaces;
using UseCases.FileStorageInterfaces;

namespace Plugins.DataStore.SQL;

public class MoviesSqlRepository: CustomBaseSqlRepository<Movie, MovieCreationDto, MovieDto>, IMoviesRepository
{
    private readonly IFileStorage _fileStorage;
    private const string Container = "movies";

    public MoviesSqlRepository(ApplicationDbContext context, IMapper mapper, IFileStorage fileStorage) : base(context, mapper)
    {
        _fileStorage = fileStorage;
    }

    public Task<List<MovieDto>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<List<MovieDto>> Get(PaginationDto paginationDto)
    {
        throw new NotImplementedException();
    }

    public override async Task<MovieDto> Add(MovieCreationDto movieCreationDto)
    {
        var movie = Mapper.Map<Movie>(movieCreationDto);

        if (movieCreationDto.Poster is not null)
        {
            var url = await _fileStorage.Store(Container, movieCreationDto.Poster);
            movie.Poster = url;
        }
        
        AssignActorOrder(movie);

        Context.Add(movie);
        await Context.SaveChangesAsync();

        return Mapper.Map<MovieDto>(movie);
    }

    private void AssignActorOrder(Movie movie)
    {
        for (int i = 0; i < movie.MoviesActors.Count; i++)
        {
            movie.MoviesActors[i].Order = i;
        }
    }
}