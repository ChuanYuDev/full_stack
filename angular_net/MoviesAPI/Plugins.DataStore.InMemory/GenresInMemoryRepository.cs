using AutoMapper;
using CoreBusiness;
using CoreBusiness.DTOs;
using Plugins.DataStore.InMemory.Utilities;
using UseCases.DataStoreInterfaces;

namespace Plugins.DataStore.InMemory;

public class GenresInMemoryRepository: IGenresRepository
{
    private readonly IMapper _mapper;
    private readonly List<Genre> _genres;

    public GenresInMemoryRepository(IMapper mapper)
    {
        _mapper = mapper;
        _genres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Comedy" },
            new Genre { Id = 2, Name = "Action" },
        };
    }
    
    public async Task<int> Count()
    {
        return _genres.Count;
    }

    public async Task<bool> Exists(int id)
    {
        return _genres.Any(g => g.Id == id);
    }

    public async Task<List<GenreDto>> GetAll()
    {
        return _mapper.Map<List<GenreDto>>(_genres);
    }

    public async Task<List<GenreDto>> Get(PaginationDto paginationDto)
    {
        return _mapper.Map<List<GenreDto>>(_genres.OrderBy(g=> g.Name).Paginate(paginationDto));
    }

    public async Task<GenreDto?> GetById(int id)
    {
        return _mapper.Map<GenreDto>(_genres.FirstOrDefault(g => g.Id == id));
    }

    public async Task<GenreDto> Add(GenreCreationDto genreCreationDto)
    {
        var genre = _mapper.Map<Genre>(genreCreationDto);
        
        var id = _genres.Max(g => g.Id) + 1;
        genre.Id = id;
        _genres.Add(genre);
        
        return _mapper.Map<GenreDto>(genre);
    }

    public async Task Update(int id, GenreCreationDto genreCreationDto)
    {
        Genre genre = _genres.First(g => g.Id == id);
        genre.Name = genreCreationDto.Name;
    }

    public async Task<int> Delete(int id)
    {
        return _genres.RemoveAll(g => g.Id == id);
    }
}