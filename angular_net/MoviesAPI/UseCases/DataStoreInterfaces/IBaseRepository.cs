using System.Linq.Expressions;
using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IBaseRepository<TEntity, TCreationDto, TDto, TDetailsDto> where TDetailsDto: TDto
{
    Task<int> Count();
    Task<List<TDto>> Get(Expression<Func<TEntity, bool>>? where = null, int top = 0);
    Task<List<TDto>> Get(PaginationDto paginationDto);
    Task<TDetailsDto?> Get(int id);
    Task<TDto> Add(TCreationDto entityCreationDto);
    Task<bool> Update(int id, TCreationDto entityCreationDto);
    Task<bool> Delete(int id);
}
