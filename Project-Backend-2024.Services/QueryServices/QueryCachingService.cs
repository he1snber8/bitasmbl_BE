using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.Services.Caching;

namespace Project_Backend_2024.Services.QueryServices;

public class QueryCachingService<TSource,TDestination>
{
    private readonly IMemoryCache _cache;
    private readonly CachingService _cachingService;
    private readonly IMapper _mapper;
    protected QueryCachingService(IMemoryCache cache, CachingService cachingService, IMapper mapper)
    {
        _cache = cache;
        _cachingService = cachingService;
        _mapper = mapper;
    }
    protected List<TDestination?>? InitializeQueryCaching(string key, List<TSource> source)
    {
        if (_cache.TryGetValue(key, out List<TDestination?>? appliedProjects))
        {
            Console.WriteLine("cache hit!");
            return appliedProjects;
        }
        
        Console.WriteLine("cache miss, fetching from db...");

        var models = _mapper.Map<List<TDestination?>?>(source);
        
        var cacheEntry = _cachingService.BuildCacheOptions();
        
        _cache.Set(key, models, cacheEntry);

        return models;
    }
}