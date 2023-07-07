using Flurl.Http;
using SpecFlowWithTestRails.Environment;

namespace SpecFlowWithTestRails.TestRailIntegration.Client;

public sealed class TestRailClient : ITestRailClient
{
    private readonly EnvironmentSettings _settings;

    public TestRailClient(EnvironmentSettings settings) => _settings = settings;

    public async Task<IFlurlResponse> GetAsync(string uri) =>
       await $"{_settings.TestRailBaseUrl}{uri}"
           .WithBasicAuth(_settings.TestRailUser, _settings.TestRailPassword)
           .GetAsync();

    public async Task<IFlurlResponse> PostAsync(string uri, object data) =>
        await $"{_settings.TestRailBaseUrl}{uri}"
            .WithBasicAuth(_settings.TestRailUser, _settings.TestRailPassword)
            .PostJsonAsync(data);
}
