using CoreBusiness.Interfaces;
using Microsoft.AspNetCore.OutputCaching;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

public class Controller<TEntity, TCreationDto, TDto>: BaseController<TEntity, TCreationDto, TDto, TDto>
    where TDto: IId
{
    public Controller(IRepository<TEntity, TCreationDto, TDto> entitiesRepository, IOutputCacheStore outputCacheStore, string cacheTag)
        :base (entitiesRepository, outputCacheStore, cacheTag)
    {
    }

}
