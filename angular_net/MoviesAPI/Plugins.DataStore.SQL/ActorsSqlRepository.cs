using AutoMapper;
using CoreBusiness;
using CoreBusiness.DTOs;
using UseCases.DataStoreInterfaces;
using UseCases.FileStorageInterfaces;

namespace Plugins.DataStore.SQL;

public class ActorsSqlRepository: IActorsRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileStorage _fileStorage;
    private readonly string container = "actors";

    public ActorsSqlRepository(ApplicationDbContext context, IMapper mapper, IFileStorage fileStorage)
    {
        _context = context;
        _mapper = mapper;
        _fileStorage = fileStorage;
    }
    
    public async Task Add(ActorCreationDto actorCreationDto)
    {
        var actor = _mapper.Map<Actor>(actorCreationDto);
        
        if (actorCreationDto.Picture is not null)
        {
            var url = await _fileStorage.Store(container, actorCreationDto.Picture);
            actor.Picture = url;
        }

        _context.Add(actor);

        await _context.SaveChangesAsync();
    }
}