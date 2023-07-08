using Flurl.Http;
using Polly.Retry;
using SpecFlowWithTestRails.Environment;
using SpecFlowWithTestRails.TestRailIntegration.Resilience;

namespace SpecFlowWithTestRails.TestRailIntegration.Client;

public sealed class TestRailClient : ITestRailClient
{
    private readonly AsyncRetryPolicy _policy;
    private readonly EnvironmentSettings _settings;

    public TestRailClient(RetryPolicyBuilder retryPolicyBuilder, EnvironmentSettings settings)
    {
        _policy = retryPolicyBuilder.BuildRetryPolicy();
        _settings = settings;
    }

    public async Task<IFlurlResponse> GetAsync(string uri) =>
        await _policy.ExecuteAsync(() => $"{_settings.TestRailBaseUrl}{uri}"
            .WithBasicAuth(_settings.TestRailUser, _settings.TestRailPassword)
            .GetAsync());

    public async Task<IFlurlResponse> PostAsync(string uri, object data) =>
        await _policy.ExecuteAsync(() => $"{_settings.TestRailBaseUrl}{uri}"
            .WithBasicAuth(_settings.TestRailUser, _settings.TestRailPassword)
            .PostJsonAsync(data));
}
