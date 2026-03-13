using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreBusiness;
using CoreBusiness.DTOs;
using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.SQL.Utilities;
using UseCases.DataStoreInterfaces;
using UseCases.FileStorageInterfaces;

namespace Plugins.DataStore.SQL;

public class ActorsSqlRepository: CustomBaseSqlRepository<Actor, ActorCreationDto, ActorDto>, IRepository<ActorCreationDto, ActorDto>
{
    private readonly IFileStorage _fileStorage;
    private const string Container = "actors";

    public ActorsSqlRepository(ApplicationDbContext context, IMapper mapper, IFileStorage fileStorage): base(context, mapper)
    {
        _fileStorage = fileStorage;
    }

    public async Task<List<ActorDto>> GetAll()
    {
        return await GetAll(a => a.Name);
    }

    public Task<List<ActorDto>> Get(PaginationDto paginationDto)
    {
        return  Get(paginationDto, orderBy: a => a.Name);
    }

    public override async Task<ActorDto> Add(ActorCreationDto actorCreationDto)
    {
        var actor = Mapper.Map<Actor>(actorCreationDto);
        
        if (actorCreationDto.Picture is not null)
        {
            var url = await _fileStorage.Store(Container, actorCreationDto.Picture);
            actor.Picture = url;
        }

        Context.Add(actor);

        await Context.SaveChangesAsync();

        return Mapper.Map<ActorDto>(actor);
    }

    public override async Task<bool> Update(int id, ActorCreationDto actorCreationDto)
    {
        var actor = await EntityDbSet.FirstOrDefaultAsync(a => a.Id == id);

        if (actor is null)
        {
            return false;
        }
        
        Mapper.Map(actorCreationDto, actor);

        if (actorCreationDto.Picture is not null)
        {
            actor.Picture = await _fileStorage.Edit(actor.Picture, Container, actorCreationDto.Picture);
        }

        await Context.SaveChangesAsync();
        return true;
    }

    public override async Task<bool> Delete(int id)
    {
        var actor = await EntityDbSet.FirstOrDefaultAsync(a => a.Id == id);

        if (actor is null)
        {
            return false;
        }
        
        await _fileStorage.Delete(actor.Picture, Container);

        Context.Remove(actor);
        await Context.SaveChangesAsync();

        return true;
    }

}