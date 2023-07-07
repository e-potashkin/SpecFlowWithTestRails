using BoDi;
using SpecFlowWithTestRails.Environment;
using SpecFlowWithTestRails.TestRailIntegration.Cache;
using SpecFlowWithTestRails.TestRailIntegration.Client;

namespace SpecFlowWithTestRails.Support;

[Binding]
internal sealed class DependencyInjection
{
    [BeforeTestRun]
    public static void RegisterDependencies(IObjectContainer container)
    {
        container.RegisterTypeAs<EnvironmentSettings, IEnvironmentSettings>();
        container.RegisterTypeAs<TestRailClient, ITestRailClient>();
        container.RegisterTypeAs<CacheService, ICacheService>();
    }
}
