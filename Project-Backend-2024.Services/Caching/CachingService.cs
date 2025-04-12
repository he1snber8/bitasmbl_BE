using Microsoft.Extensions.Caching.Memory;
using Project_Backend_2024.Services.Configurations;

namespace Project_Backend_2024.Services.Caching;

public sealed class CachingService(CacheConfiguration cacheConfiguration)
{
    public MemoryCacheEntryOptions BuildCacheOptions()
    {
        return new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromSeconds(cacheConfiguration.SlidingExpirationSeconds))
            .SetAbsoluteExpiration(TimeSpan.FromSeconds(cacheConfiguration.AbsoluteExpirationSeconds))
            .SetPriority(CacheItemPriority.Normal);
    }
}