namespace SpecFlowWithTestRails.Environment;

public interface IEnvironmentSettings
{
    string TestRailBaseUrl { get; }

    string TestRailUser { get; }

    string TestRailPassword { get; }

    int TestRailProjectId { get; }

    int TestRailSuiteId { get; }

    string TestRailTestRunTitle { get; }

    bool IsDevelopment { get; }
}
