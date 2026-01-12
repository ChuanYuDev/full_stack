using AutoMapper;
using CoreBusiness;
using CoreBusiness.DTOs;
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

    public async Task<List<GenreDto>> GetAll()
    {
        return _mapper.Map<List<GenreDto>>(_genres);
    }

    public async Task<Genre?> GetById(int id)
    {
        await Task.Delay(TimeSpan.FromSeconds(3));
        return _genres.FirstOrDefault(g => g.Id == id);
    }

    public bool Exists(string name)
    {
        return _genres.Any(g => g.Name == name);
    }

    public async Task<GenreDto> Add(GenreCreationDto genreCreationDto)
    {
        var genre = _mapper.Map<Genre>(genreCreationDto);
        
        var id = _genres.Max(g => g.Id) + 1;
        genre.Id = id;
        _genres.Add(genre);
        
        return _mapper.Map<GenreDto>(genre);
    }
}