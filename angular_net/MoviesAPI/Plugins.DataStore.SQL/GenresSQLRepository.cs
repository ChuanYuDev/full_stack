using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreBusiness;
using CoreBusiness.DTOs;
using Microsoft.EntityFrameworkCore;
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

    public async Task<List<GenreDto>> GetAll()
    {
        return await _context.Genres.ProjectTo<GenreDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<Genre?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public bool Exists(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<GenreDto> Add(GenreCreationDto genreCreationDto)
    {
        var genre = _mapper.Map<Genre>(genreCreationDto);
        _context.Add(genre);
        await _context.SaveChangesAsync();

        return _mapper.Map<GenreDto>(genre);
    }
}