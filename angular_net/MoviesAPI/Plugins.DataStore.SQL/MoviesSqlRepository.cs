using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreBusiness;
using CoreBusiness.DTOs;
using Microsoft.EntityFrameworkCore;
using UseCases.DataStoreInterfaces;
using UseCases.FileStorageInterfaces;

namespace Plugins.DataStore.SQL;

public class MoviesSqlRepository: IMoviesRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileStorage _fileStorage;
    private const string Container = "movies";

    public MoviesSqlRepository(ApplicationDbContext context, IMapper mapper, IFileStorage fileStorage)
    {
        _context = context;
        _mapper = mapper;
        _fileStorage = fileStorage;
    }

    public async Task<List<MovieDto>> Get(Expression<Func<Movie, bool>> where, int top)
    {
        return await _context.Movies
            .Where(where)
            .OrderBy(m => m.ReleaseDate)
            .Take(top)
            .ProjectTo<MovieDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<MovieDetailsDto?> Get(int id)
    {
        return await _context.Movies
            .AsSplitQuery()
            .ProjectTo<MovieDetailsDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<MovieDto> Add(MovieCreationDto movieCreationDto)
    {
        var movie = _mapper.Map<Movie>(movieCreationDto);

        if (movieCreationDto.Poster is not null)
        {
            var url = await _fileStorage.Store(Container, movieCreationDto.Poster);
            movie.Poster = url;
        }
        
        AssignActorOrder(movie);

        _context.Add(movie);
        await _context.SaveChangesAsync();

        return _mapper.Map<MovieDto>(movie);
    }

    private void AssignActorOrder(Movie movie)
    {
        for (int i = 0; i < movie.MoviesActors.Count; i++)
        {
            movie.MoviesActors[i].Order = i;
        }
    }
}