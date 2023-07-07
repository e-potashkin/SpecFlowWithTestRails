using Microsoft.Extensions.Caching.Memory;

namespace SpecFlowWithTestRails.TestRailIntegration.Cache;

public interface ICacheService
{
    Task<T?> GetOrCreateAsync<T>(string key, Func<ICacheEntry, Task<T>> create);

    Task CacheSectionsAsync();

    Task CacheTestCasesAsync();
}
