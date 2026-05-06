using CoreBusiness;
using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IActorsRepository 
{
    Task<int> Count();
    Task<List<ActorDto>> Get();
    Task<List<ActorDto>> Get(PaginationDto paginationDto);
    Task<List<MovieActorDto>> Get(string name);
    Task<ActorDto?> Get(int id);
    Task<ActorDto> Add(ActorCreationDto actorCreationDto);
    Task<bool> Update(int id, ActorCreationDto actorCreationDto);
    Task<bool> Delete(int id);

}