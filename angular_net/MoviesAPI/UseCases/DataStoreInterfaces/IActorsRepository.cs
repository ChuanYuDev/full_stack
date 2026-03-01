using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IActorsRepository
{
    Task<int> Count();
    Task<bool> Exists(int id);
    Task<List<ActorDto>> Get(PaginationDto paginationDto);
    Task<ActorDto?> GetById(int id);
    Task<ActorDto> Add(ActorCreationDto actorCreationDto);
    Task Update(int id, ActorCreationDto actorCreationDto);
    Task<int> Delete(int id);
}