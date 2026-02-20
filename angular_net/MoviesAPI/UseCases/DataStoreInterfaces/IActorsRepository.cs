using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IActorsRepository
{
    Task Add(ActorCreationDto actorCreationDto);
}