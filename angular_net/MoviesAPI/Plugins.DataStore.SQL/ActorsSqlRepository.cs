using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreBusiness;
using CoreBusiness.DTOs;
using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.SQL.Utilities;
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

    public async Task<int> Count()
    {
        return await _context.Actors.CountAsync();
    }

    public Task<bool> Exists(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ActorDto>> Get(PaginationDto paginationDto)
    {
        return await _context.Actors
            .OrderBy(actor => actor.Name)
            .Paginate(paginationDto)
            .ProjectTo<ActorDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<ActorDto?> GetById(int id)
    {
        return await _context.Actors
            .ProjectTo<ActorDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(actorDto => actorDto.Id == id);
    }

    public async Task<ActorDto> Add(ActorCreationDto actorCreationDto)
    {
        var actor = _mapper.Map<Actor>(actorCreationDto);
        
        if (actorCreationDto.Picture is not null)
        {
            var url = await _fileStorage.Store(container, actorCreationDto.Picture);
            actor.Picture = url;
        }

        _context.Add(actor);

        await _context.SaveChangesAsync();

        return _mapper.Map<ActorDto>(actor);
    }

    public async Task Update(int id, ActorCreationDto actorCreationDto)
    {
        var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);
        _mapper.Map(actorCreationDto, actor);
        throw new NotImplementedException();
    }

    public Task<int> Delete(int id)
    {
        throw new NotImplementedException();
    }

}