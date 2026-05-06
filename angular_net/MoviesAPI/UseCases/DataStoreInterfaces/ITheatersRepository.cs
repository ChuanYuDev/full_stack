using System.Linq.Expressions;
using CoreBusiness;
using CoreBusiness.DTOs;

namespace UseCases.DataStoreInterfaces;

public interface ITheatersRepository
{
    Task<int> Count();
    Task<List<TheaterDto>> Get();
    Task<List<TheaterDto>> Get(Expression<Func<Theater, bool>> where);
    Task<List<TheaterDto>> Get(PaginationDto paginationDto);
    Task<TheaterDto?> Get(int id);
    Task<TheaterDto> Add(TheaterCreationDto theaterCreationDto);
    Task<bool> Update(int id, TheaterCreationDto theaterCreationDto);
    Task<bool> Delete(int id);

}