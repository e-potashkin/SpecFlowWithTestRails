namespace SpecFlowWithTestRails;

public sealed class ContextProvider
{
    private const string Issue = "Issue_";
    private readonly FeatureContext _featureContext;
    private readonly ScenarioContext _scenarioContext;

    public ContextProvider(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        _featureContext = featureContext;
        _scenarioContext = scenarioContext;
    }

    public string FeatureTitle => _featureContext.FeatureInfo.Title;

    public string TestCaseTitle => _scenarioContext.ScenarioInfo.Title;

    public Exception? TestError => _scenarioContext.TestError;

    public string? Reference => _scenarioContext.ScenarioInfo.Tags
             .Where(i => i.StartsWith(nameof(Issue))) //get all entries that start with Issue__
             .Select(i => i[6..]) //get only the part after Issue_
             .FirstOrDefault();

    public string GetStepText()
    {
        var stepInstance = _scenarioContext.StepContext.StepInfo.StepInstance;
        var stepText = $"{stepInstance.Keyword}{stepInstance.Text}";
        if (stepInstance.TableArgument is not null)
        {
            var tableText = stepInstance.TableArgument.ToString();
            stepText += System.Environment.NewLine + tableText;
        }

        return stepText;
    }
}
