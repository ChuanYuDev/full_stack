using System.Linq.Expressions;
using AutoMapper;
using CoreBusiness;
using CoreBusiness.DTOs;
using UseCases.DataStoreInterfaces;

namespace Plugins.DataStore.SQL;

public class GenresSqlRepository: SqlRepository<Genre, GenreCreationDto, GenreDto>, IGenresRepository 
{
    public GenresSqlRepository(ApplicationDbContext context, IMapper mapper): base(context, mapper)
    {
    }

    public async Task<List<GenreDto>> Get(Expression<Func<Genre, bool>>? where = null, int top = 0)
    {
        return await Get(where: where, orderBy: g => g.Name, top: top);
    }

    public async Task<List<GenreDto>> Get(PaginationDto paginationDto)
    {
        return await Get(paginationDto, g => g.Name);
    }
}