using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreBusiness;
using CoreBusiness.DTOs;
using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.SQL.Utilities;
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
    
    public async Task<(List<MovieDto> Movies, int MoviesNum)> Filter(MoviesFilterDto moviesFilterDto)
    {
        var moviesQueryable = EntityDbSet.AsQueryable();

        if (!string.IsNullOrEmpty(moviesFilterDto.Title))
        {
            moviesQueryable = moviesQueryable.Where(m => m.Title.Contains(moviesFilterDto.Title));
        }

        if (moviesFilterDto.GenreId != 0)
        {
            moviesQueryable = moviesQueryable.Where(m => m.MoviesGenres.Select(mg => mg.GenreId).Contains(moviesFilterDto.GenreId));
        }
        
        if (moviesFilterDto.InTheaters)
        {
            moviesQueryable = moviesQueryable.Where(m => m.MoviesTheaters.Count > 0);
        }

        if (moviesFilterDto.UpcomingReleases)
        {
            var today = DateTime.Today;
            moviesQueryable = moviesQueryable.Where(m => m.ReleaseDate > today);
        }
        
        var movies = await moviesQueryable
            .OrderBy(m => m.ReleaseDate)
            .Paginate(moviesFilterDto.PaginationDto)
            .ProjectTo<MovieDto>(Mapper.ConfigurationProvider)
            .ToListAsync();

        var moviesNum = await moviesQueryable.CountAsync();

        return (movies, moviesNum);
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

    public override async Task<bool> Delete(int id)
    {
        var movie = await Context.Movies.FirstOrDefaultAsync(m => m.Id == id);

        if (movie is null)
        {
            return false;
        }

        Context.Remove(movie);
        await Context.SaveChangesAsync();
        await _fileStorage.Delete(movie.Poster, Container);

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