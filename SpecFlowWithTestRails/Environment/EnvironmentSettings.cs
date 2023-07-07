using System.Reflection;
using DotNetEnv;

namespace SpecFlowWithTestRails.Environment;

public sealed class EnvironmentSettings : IEnvironmentSettings
{
    private const string EnvironmentDirectory = "Environment";

    public EnvironmentSettings()
    {
        Console.WriteLine("Getting configs");

        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var envPath = Path.Combine(assemblyPath!, EnvironmentDirectory, ".env.local");
        if (!File.Exists(envPath))
        {
            throw new Exception($"Configuration file {envPath} wasn't found");
        }

        Env.Load(envPath);

        TestRailBaseUrl = Env.GetString(nameof(TestRailBaseUrl));
        TestRailUser = Env.GetString(nameof(TestRailUser));
        TestRailPassword = Env.GetString(nameof(TestRailPassword));
        TestRailProjectId = Env.GetInt(nameof(TestRailProjectId));
        TestRailSuiteId = Env.GetInt(nameof(TestRailSuiteId));
        TestRailTestRunTitle = Env.GetString(nameof(TestRailTestRunTitle));
        IsDevelopment = Env.GetBool(nameof(IsDevelopment));
    }

    public string TestRailBaseUrl { get; }

    public string TestRailUser { get; }

    public string TestRailPassword { get; }

    public int TestRailProjectId { get; }

    public int TestRailSuiteId { get; }

    public string TestRailTestRunTitle { get; }

    public bool IsDevelopment { get; }
}
