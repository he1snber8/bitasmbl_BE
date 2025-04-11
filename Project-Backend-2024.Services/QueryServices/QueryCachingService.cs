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
    protected TDestination?  InitializeQueryCaching(string key, TSource? source)
    {
        if (_cache.TryGetValue(key, out TDestination? destination))
        {
            Console.WriteLine("cache hit!");
            return destination;
        }
        
        Console.WriteLine("cache miss, fetching from db...");

        var result = _mapper.Map<TDestination>(source);
        
        var cacheEntry = _cachingService.BuildCacheOptions();
        
        _cache.Set(key, result, cacheEntry);

        return result;
    }
}