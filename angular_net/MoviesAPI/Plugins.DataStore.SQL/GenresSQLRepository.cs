using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreBusiness;
using CoreBusiness.DTOs;
using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.SQL.Utilities;
using UseCases.DataStoreInterfaces;

namespace Plugins.DataStore.SQL;

public class GenresSqlRepository: IGenresRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GenresSqlRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Count()
    {
        return await _context.Genres.CountAsync();
    }
    
    public async Task<bool> Exists(int id)
    {
        return await _context.Genres.AnyAsync(g => g.Id == id);
    }

    public async Task<List<GenreDto>> GetAll()
    {
        return await _context.Genres.ProjectTo<GenreDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<List<GenreDto>> Get(PaginationDto paginationDto)
    {
        return await _context.Genres
            .OrderBy(g=>g.Name)
            .Paginate(paginationDto)
            .ProjectTo<GenreDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<GenreDto?> GetById(int id)
    {
        return await _context.Genres
            .ProjectTo<GenreDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<GenreDto> Add(GenreCreationDto genreCreationDto)
    {
        var genre = _mapper.Map<Genre>(genreCreationDto);
        _context.Add(genre);
        await _context.SaveChangesAsync();

        return _mapper.Map<GenreDto>(genre);
    }

    public async Task Update(int id, GenreCreationDto genreCreationDto)
    {
        var genre = _mapper.Map<Genre>(genreCreationDto);
        genre.Id = id;

        _context.Update(genre);
        await _context.SaveChangesAsync();
    }

    public async Task<int> Delete(int id)
    {
        return await _context.Genres.Where(g => g.Id == id).ExecuteDeleteAsync();
    }
}