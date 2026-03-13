using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreBusiness;
using CoreBusiness.DTOs;
using Microsoft.EntityFrameworkCore;
using Plugins.DataStore.SQL.Utilities;
using UseCases.DataStoreInterfaces;

namespace Plugins.DataStore.SQL;

public class GenresSqlRepository: CustomBaseSqlRepository<Genre, GenreCreationDto, GenreDto>, IRepository<GenreCreationDto, GenreDto>
{
    public GenresSqlRepository(ApplicationDbContext context, IMapper mapper): base(context, mapper)
    {
    }

    public async Task<List<GenreDto>> GetAll()
    {
        return await GetAll(g => g.Name);
    }

    public async Task<List<GenreDto>> Get(PaginationDto paginationDto)
    {
        return await Get(paginationDto, g => g.Name);
    }
}