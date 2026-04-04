using CoreBusiness.DTOs;
using UseCases.DataStoreInterfaces;

namespace Plugins.DataStore.InMemory;

public class ActorsInMemoryRepository: IActorsRepository
{
    public Task<int> Count()
    {
        throw new NotImplementedException();
    }

    public Task<List<ActorDto>> Get()
    {
        throw new NotImplementedException();
    }

    public Task<List<ActorDto>> Get(PaginationDto paginationDto)
    {
        throw new NotImplementedException();
    }

    public Task<ActorDto?> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ActorDto> Add(ActorCreationDto entityCreationDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(int id, ActorCreationDto entityCreationDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<MovieActorDto>> Get(string name)
    {
        throw new NotImplementedException();
    }
}