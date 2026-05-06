using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface IUsersRepository
{
    Task<int> Count();
    Task<List<UserDto>> Get(PaginationDto paginationDto);
    
    Task<string> GetUserId();
}