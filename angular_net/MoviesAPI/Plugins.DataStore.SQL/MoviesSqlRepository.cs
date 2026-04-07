using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreBusiness;
using CoreBusiness.DTOs;
using Microsoft.EntityFrameworkCore;
using UseCases.DataStoreInterfaces;
using UseCases.FileStorageInterfaces;

namespace Plugins.DataStore.SQL;

public class MoviesSqlRepository: BaseSqlRepository<Movie, MovieCreationDto, MovieDto, MovieDetailsDto>, IMoviesRepository
{
    private readonly IFileStorage _fileStorage;
    private const string Container = "movies";

    public MoviesSqlRepository(ApplicationDbContext context, IMapper mapper, IFileStorage fileStorage): base(context, mapper)
    {
        _fileStorage = fileStorage;
    }

    public async Task<List<MovieDto>> Get(Expression<Func<Movie, bool>>? where = null, int top = 0)
    {
        return await Get(where: where, orderBy: m => m.ReleaseDate, top);
    }

    public async Task<List<MovieDto>> Get(PaginationDto paginationDto)
    {
        return await Get(paginationDto, orderBy: m => m.ReleaseDate);
    }

    public override async Task<MovieDetailsDto?> Get(int id)
    {
        return await EntityDbSet 
            .AsSplitQuery()
            .ProjectTo<MovieDetailsDto>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(m => m.Id == id);
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

    public override async Task<bool> Update(int id, MovieCreationDto movieCreationDto)
    {
        var movie = await EntityDbSet
            .Include(m => m.MoviesGenres)
            .Include(m => m.MoviesTheaters)
            .Include(m => m.MoviesActors)
            .AsSplitQuery()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (movie is null)
        {
            return false;
        }

        Mapper.Map(movieCreationDto, movie);

        if (movieCreationDto.Poster is not null)
        {
            movie.Poster = await _fileStorage.Edit(movie.Poster, Container, movieCreationDto.Poster);
        }
        
        AssignActorOrder(movie);
        await Context.SaveChangesAsync();
        return true;
    }

    private void AssignActorOrder(Movie movie)
    {
        for (int i = 0; i < movie.MoviesActors.Count; i++)
        {
            movie.MoviesActors[i].Order = i;
        }
    }
}