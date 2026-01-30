using CoreBusiness.DTOs;

namespace Plugins.DataStore.InMemory.Utilities;

public static class IEnumerableExtensions
{
    public static IEnumerable<T> Paginate<T>(this IEnumerable<T> enumerable, PaginationDto paginationDto)
    {
        return enumerable
            .Skip((paginationDto.Page - 1) * paginationDto.RecordsPerPage)
            .Take(paginationDto.RecordsPerPage);
    }
}