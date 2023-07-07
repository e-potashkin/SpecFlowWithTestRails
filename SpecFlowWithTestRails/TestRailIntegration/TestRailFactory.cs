using System.Collections;
using SpecFlowWithTestRails.Environment;
using SpecFlowWithTestRails.TestRailIntegration.Client;
using SpecFlowWithTestRails.TestRailIntegration.Models;

namespace SpecFlowWithTestRails.TestRailIntegration;

public sealed class TestRailFactory
{
    private readonly TestRailClient _testRailClient;
    private readonly EnvironmentSettings _environmentSettings;

    public TestRailFactory(TestRailClient testRailClient, EnvironmentSettings environmentSettings)
    {
        _testRailClient = testRailClient;
        _environmentSettings = environmentSettings;
    }

    public async Task<int> CreateTestRunAsync(IEnumerable caseIds)
    {
        var testResult = new Dictionary<string, object>
        {
            ["suite_id"] = _environmentSettings.TestRailSuiteId,
            ["name"] = _environmentSettings.TestRailTestRunTitle,
            ["include_all"] = false,
            ["case_ids"] = caseIds
        };

        var response = await _testRailClient.PostAsync($"add_run/{_environmentSettings.TestRailProjectId}", testResult);
        var testRun = await response.GetJsonAsync<CreateResponse>();

        return testRun.Id;
    }

    public async Task<int> CreateSectionAsync(string featureTitle)
    {
        var testResult = new Dictionary<string, object>
        {
            ["suite_id"] = _environmentSettings.TestRailSuiteId,
            ["name"] = featureTitle
        };

        var response = await _testRailClient.PostAsync($"add_section/{_environmentSettings.TestRailProjectId}", testResult);
        var createdSection = await response.GetJsonAsync<CreateResponse>();

        return createdSection.Id;
    }

    public async Task<int> CreateTestCaseAsync(
        int sectionId,
        string testCaseTitle,
        string? reference,
        string scenarioSteps)
    {
        var testResult = new Dictionary<string, object>
        {
            ["title"] = testCaseTitle,
            ["refs"] = reference ?? string.Empty,
            ["custom_steps"] = scenarioSteps,
            ["custom_expected"] = string.Empty
        };

        var response = await _testRailClient.PostAsync($"add_case/{sectionId}", testResult);
        var createdTestCase = await response.GetJsonAsync<CreateResponse>();

        return createdTestCase.Id;
    }
}