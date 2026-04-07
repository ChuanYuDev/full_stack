using CoreBusiness;
using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IActorsRepository: IRepository<Actor, ActorCreationDto, ActorDto>
{
    public Task<List<MovieActorDto>> Get(string name);

}