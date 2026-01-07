using CoreBusiness;
using UseCases.DataStoreInterfaces;

namespace Plugins.DataStore.SQL;

public class GenresSqlRepository: IGenresRepository
{
    private readonly ApplicationDbContext _context;
    public GenresSqlRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<Genre> GetAllGenres()
    {
        throw new NotImplementedException();
    }

    public async Task<Genre?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public bool Exists(string name)
    {
        throw new NotImplementedException();
    }

    public async Task Add(Genre genre)
    {
        _context.Add(genre);
        await _context.SaveChangesAsync();
    }
}