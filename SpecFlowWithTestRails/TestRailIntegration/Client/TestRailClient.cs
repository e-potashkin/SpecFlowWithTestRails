using Flurl.Http;
using SpecFlowWithTestRails.Environment;
using SpecFlowWithTestRails.TestRailIntegration.Resilience;

namespace SpecFlowWithTestRails.TestRailIntegration.Client;

public sealed class TestRailClient : ITestRailClient
{
    private readonly RetryPolicyBuilder _retryPolicyBuilder;
    private readonly EnvironmentSettings _settings;

    public TestRailClient(RetryPolicyBuilder retryPolicyBuilder, EnvironmentSettings settings)
    {
        _retryPolicyBuilder = retryPolicyBuilder;
        _settings = settings;
    }

    public async Task<IFlurlResponse> GetAsync(string uri)
    {
        var policy = _retryPolicyBuilder.BuildRetryPolicy();
        return await policy.ExecuteAsync(() => $"{_settings.TestRailBaseUrl}{uri}"
            .WithBasicAuth(_settings.TestRailUser, _settings.TestRailPassword)
            .GetAsync()
        );
    }

    public async Task<IFlurlResponse> PostAsync(string uri, object data)
    {
        var policy = _retryPolicyBuilder.BuildRetryPolicy();
        return await policy.ExecuteAsync(() => $"{_settings.TestRailBaseUrl}{uri}"
            .WithBasicAuth(_settings.TestRailUser, _settings.TestRailPassword)
            .PostJsonAsync(data));
    }
}
