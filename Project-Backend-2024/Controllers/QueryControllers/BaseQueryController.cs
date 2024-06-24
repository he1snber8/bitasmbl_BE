using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.DTO.Interfaces;
using Project_Backend_2024.Facade.Models;
using Project_Backend_2024.Services.Caching;
using Project_Backend_2024.Services.Interfaces.Queries;

namespace Project_Backend_2024.Controllers.QueryControllers;

[ApiController]
[Route("queries/[controller]")]
public abstract class BaseQueryController<TEntity, TEntityModel, TBasicModel, TQueryService>(
    TQueryService queryService,
    IMapper mapper,
    CachingService cachingService,
    CacheConfiguration cacheConfiguration,
    IMemoryCache cache)
    : Controller
    where TEntityModel : class, IEntityModel
    where TEntity : class, IEntity
    where TBasicModel : IBasicGetModel
    where TQueryService : IQueryModel<TEntity, TEntityModel>
{
    private readonly TQueryService _queryService = queryService;

   
    protected async Task<List<TBasicModel>?> GetAll()
    {
        if (cache.TryGetValue(cacheConfiguration.CacheKey, out List<TBasicModel>? cachedEntities))
        {
            Console.WriteLine("Cache Hit!.");
            return cachedEntities;
        }

        Console.WriteLine("Cache miss! fetching from DB.");
        
        var entities = await _queryService.GetAll();
        var result = mapper.Map<List<TBasicModel>?>(entities);

        var cacheEntryOptions = cachingService.BuildCacheOptions();
        cache.Set(cacheConfiguration.CacheKey, result, cacheEntryOptions);

        return result;
    }


    protected async Task<TBasicModel?> GetById(int id)
    {
        var model = await _queryService.GetByIdAsync(id);

        var result = mapper.Map<TBasicModel>(model);

        return result;
    }
}