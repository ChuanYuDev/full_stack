using CoreBusiness;
using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IActorsRepository: IRepository<Actor, ActorCreationDto, ActorDto>
{
    Task<List<MovieActorDto>> Get(string name);

}