using Microsoft.Extensions.Caching.Memory;
using SpecFlowWithTestRails.Environment;
using SpecFlowWithTestRails.TestRailIntegration.Client;
using SpecFlowWithTestRails.TestRailIntegration.Models;

namespace SpecFlowWithTestRails.TestRailIntegration.Cache;

public sealed class CacheService : ICacheService
{
    private readonly TestRailClient _client;
    private readonly EnvironmentSettings _environmentSettings;
    private static readonly Lazy<MemoryCache> Cache = new(() => new MemoryCache(new MemoryCacheOptions()));

    public CacheService(TestRailClient client, EnvironmentSettings environmentSettings)
    {
        _client = client;
        _environmentSettings = environmentSettings;
    }

    public object Get(string key) => Cache.Value.Get(key)!;

    public async Task<T?> GetOrCreateAsync<T>(string key, Func<ICacheEntry, Task<T>> create) =>
        await Cache.Value.GetOrCreateAsync(key, create);

    public async Task CacheSectionsAsync()
    {
        var response = await _client.GetAsync($"get_sections/{_environmentSettings.TestRailProjectId}&suite_id={_environmentSettings.TestRailSuiteId}");
        var sectionsResponse = await response.GetJsonAsync<GetSectionsResponse>();

        foreach (var section in sectionsResponse.Sections)
        {
            Cache.Value.Set(section.Name, section.Id);
        }
    }

    public async Task CacheTestCasesAsync()
    {
        var response = await _client.GetAsync($"get_cases/{_environmentSettings.TestRailProjectId}&suite_id={_environmentSettings.TestRailSuiteId}");
        var testCasesResponse = await response.GetJsonAsync<GetTestCasesResponse>();

        foreach (var testCase in testCasesResponse.TestCases)
        {
            Cache.Value.Set(testCase.Title, testCase.Id);
        }
    }
}
