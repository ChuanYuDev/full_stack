using CoreBusiness;
using UseCases.DataStoreInterfaces;

namespace Plugins.DataStore.SQL;

public class GenresSQLRepository: IGenresRepository
{
    private List<Genre> _genres;

    public GenresSQLRepository()
    {
        _genres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Comedy SQL" },
            new Genre { Id = 2, Name = "Action SQL" },
        };
    }

    public List<Genre> GetAllGenres()
    {
        return _genres;
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

    public int Add(Genre genre)
    {
        throw new NotImplementedException();
    }
}