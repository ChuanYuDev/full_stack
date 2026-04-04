using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IRepository<TCreationDto, TDto>
{
    Task<int> Count();
    Task<List<TDto>> Get();
    Task<List<TDto>> Get(PaginationDto paginationDto);
    Task<TDto?> Get(int id);
    Task<TDto> Add(TCreationDto entityCreationDto);
    Task<bool> Update(int id, TCreationDto entityCreationDto);
    Task<bool> Delete(int id);
}