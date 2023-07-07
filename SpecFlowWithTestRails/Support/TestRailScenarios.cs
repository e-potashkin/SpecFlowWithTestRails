﻿using System.Text;
using SpecFlowWithTestRails.Environment;
using SpecFlowWithTestRails.TestRailIntegration;
using SpecFlowWithTestRails.TestRailIntegration.Cache;
using SpecFlowWithTestRails.TestRailIntegration.Client;

namespace SpecFlowWithTestRails.Support;

[Binding]
public class TestRailScenarios
{
    private readonly ContextProvider _contextProvider;
    private readonly EnvironmentSettings _environmentSettings;
    private readonly CacheService _cacheService;
    private static TestRailClient _testRailClient;
    private static TestRailFactory _testRailFactory;

    private readonly StringBuilder _scenarioSteps = new();
    private static readonly List<Dictionary<string, object>> Results = new();

    public TestRailScenarios(
        ContextProvider contextProvider,
        EnvironmentSettings environmentSettings,
        CacheService cacheService,
        TestRailClient testRailClient,
        TestRailFactory testRailFactory)
    {
        _contextProvider = contextProvider;
        _environmentSettings = environmentSettings;
        _cacheService = cacheService;
        _testRailClient = testRailClient;
        _testRailFactory = testRailFactory;
    }

    [AfterStep]
    public void AddStep()
    {
        var stepText = _contextProvider.GetStepText();
        _scenarioSteps.AppendLine(stepText);
    }

    [AfterScenario]
    public async Task AddTestResult()
    {
        if (_environmentSettings.IsDevelopment)
        {
            return;
        }

        var testResult = new Dictionary<string, object>();
        var testCaseId = await GetTestCaseIdAsync();
        var error = _contextProvider.TestError;
        if (error is not null)
        {
            testResult["case_id"] = testCaseId;
            testResult["status_id"] = "5"; // failed
            testResult["comment"] = error.ToString();
        }
        else
        {
            testResult["case_id"] = testCaseId;
            testResult["status_id"] = "1"; // passed
        }

        Results.Add(testResult);
    }

    [AfterTestRun]
    public static async Task PublishTestResult()
    {
        var caseIds = Results
            .SelectMany(x => x.Where(pair => pair.Key == "case_id")
            .Select(pair => pair.Value));

        var testRunId = await _testRailFactory.CreateTestRunAsync(caseIds);
        await _testRailClient.PostAsync($"add_results_for_cases/{testRunId}", new { results = Results });
    }

    private async ValueTask<int> GetTestCaseIdAsync()
    {
        var sectionId = await _cacheService.GetOrCreateAsync(
            _contextProvider.FeatureTitle,
            async _ => await _testRailFactory.CreateSectionAsync(_contextProvider.FeatureTitle));

        var testCaseId = await _cacheService.GetOrCreateAsync(
            _contextProvider.TestCaseTitle,
            async _ => await _testRailFactory.CreateTestCaseAsync(
                sectionId,
                _contextProvider.TestCaseTitle,
                _contextProvider.Reference,
                _scenarioSteps.ToString()));

        return testCaseId;
    }
}
