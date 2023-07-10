using Newtonsoft.Json;

namespace SpecFlowWithTestRails.TestRailIntegration.Models;

public record GetTestCasesResponse([JsonProperty("cases")] List<TestCase> TestCases, [JsonProperty("_links")] Link Link);
