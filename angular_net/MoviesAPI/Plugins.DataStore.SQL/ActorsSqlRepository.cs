using AutoMapper;
using CoreBusiness;
using CoreBusiness.DTOs;
using UseCases.DataStoreInterfaces;

namespace Plugins.DataStore.SQL;

public class ActorsSqlRepository: IActorsRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ActorsSqlRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task Add(ActorCreationDto actorCreationDto)
    {
        var actor = _mapper.Map<Actor>(actorCreationDto);
        
        // TO DO: work with the picture

        _context.Add(actor);

        await _context.SaveChangesAsync();
    }
}