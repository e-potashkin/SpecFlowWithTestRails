using Flurl.Http;

namespace SpecFlowWithTestRails.TestRailIntegration.Client;

public interface ITestRailClient
{
    Task<IFlurlResponse> GetAsync(string uri);

    Task<IFlurlResponse> PostAsync(string uri, object data);
}
