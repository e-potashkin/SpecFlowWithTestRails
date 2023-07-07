using BoDi;
using SpecFlowWithTestRails.Environment;
using SpecFlowWithTestRails.TestRailIntegration.Cache;

namespace SpecFlowWithTestRails.Support;

[Binding]
public class TestRailCache
{
    [BeforeTestRun]
    public static async Task PopulateTestRailCache(IObjectContainer container)
    {
        var environmentSettings = container.Resolve<IEnvironmentSettings>();
        if (environmentSettings.IsDevelopment)
        {
            return;
        }

        var service = container.Resolve<ICacheService>();
        var sectionsTask = service.CacheSectionsAsync();
        var testCasesTask = service.CacheTestCasesAsync();

        await Task.WhenAll(sectionsTask, testCasesTask);
    }
}
