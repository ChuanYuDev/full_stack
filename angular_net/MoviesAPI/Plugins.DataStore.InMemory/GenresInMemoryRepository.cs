using System.Linq.Expressions;
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
    
    public Task<int> Count()
    {
        return Task.FromResult(_genres.Count);
    }

    public Task<List<GenreDto>> Get()
    {
        throw new NotImplementedException();
    }

    public Task<List<GenreDto>> Get(Expression<Func<Genre, bool>> where)
    {
        throw new NotImplementedException();
    }

    public Task<List<GenreDto>> Get(Expression<Func<Theater, bool>> where)
    {
        throw new NotImplementedException();
    }

    public Task<List<GenreDto>> Get(PaginationDto paginationDto)
    {
        return Task.FromResult(_mapper.Map<List<GenreDto>>(_genres.OrderBy(g=> g.Name).Paginate(paginationDto)));
    }

    public Task<GenreDto?> Get(int id)
    {
        return Task.FromResult(_mapper.Map<GenreDto?>(_genres.FirstOrDefault(g => g.Id == id)));
    }

    public Task<GenreDto> Add(GenreCreationDto genreCreationDto)
    {
        var genre = _mapper.Map<Genre>(genreCreationDto);
        
        var id = _genres.Max(g => g.Id) + 1;
        genre.Id = id;
        _genres.Add(genre);
        
        return Task.FromResult(_mapper.Map<GenreDto>(genre));
    }

    public Task<bool> Update(int id, GenreCreationDto genreCreationDto)
    {
        var genre = _genres.FirstOrDefault(g => g.Id == id);

        if (genre is null)
        {
            return Task.FromResult(false);
        }
        
        _mapper.Map(genreCreationDto, genre);
        return Task.FromResult(true);
    }

    public Task<bool> Delete(int id)
    {
        var deleteRecords = _genres.RemoveAll(g => g.Id == id);

        return Task.FromResult(deleteRecords != 0);
    }
}