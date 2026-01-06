using CoreBusiness;

namespace UseCases.DataStoreInterfaces;

public interface IGenresRepository
{
    List<Genre> GetAllGenres();
    Task<Genre?> GetById(int id);
    bool Exists(string name);
    int Add(Genre genre);
}