using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IActorsRepository
{
    Task<int> Count();
    Task<List<ActorDto>> Get(PaginationDto paginationDto);
    Task<ActorDto?> GetById(int id);
    Task<ActorDto> Add(ActorCreationDto actorCreationDto);
    Task<bool> Update(int id, ActorCreationDto actorCreationDto);
    Task<int> Delete(int id);
}