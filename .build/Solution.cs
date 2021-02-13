using System.Collections.Generic;
using System.Linq;
using Rocket.Surgery.Nuke.ContinuousIntegration;
using Rocket.Surgery.Nuke.GithubActions;

[PrintBuildVersion, PrintCIEnvironment, UploadLogs]
public partial class EverCraft
{
    public static RocketSurgeonGitHubActionsConfiguration Middleware(RocketSurgeonGitHubActionsConfiguration configuration)
    {
        var buildJob = configuration.Jobs.First(z => z.Name == "Build");
        var checkoutStep = buildJob.Steps.OfType<CheckoutStep>().Single();
        // For fetch all
        checkoutStep.FetchDepth = 0;
        buildJob.Steps.InsertRange(buildJob.Steps.IndexOf(checkoutStep) + 1, new BaseGitHubActionsStep[] {
            new RunStep("Fetch all history for all tags and branches") {
                Run = "git fetch --prune"
            },
            new SetupDotNetStep("Use .NET Core 3.1 SDK") {
                DotNetVersion = "3.1.x"
            }
        });

        buildJob.Steps.Add(new UsingStep("Publish Coverage")
        {
            Uses = "codecov/codecov-action@v1",
            With = new Dictionary<string, string>
            {
                ["name"] = "actions-${{ matrix.os }}",
            }
        });

        buildJob.Steps.Add(new UploadArtifactStep("Publish logs")
        {
            Name = "logs",
            Path = "artifacts/logs/",
            If = "always()"
        });

        buildJob.Steps.Add(new UploadArtifactStep("Publish coverage data")
        {
            Name = "coverage",
            Path = "coverage/",
            If = "always()"
        });

        buildJob.Steps.Add(new UploadArtifactStep("Publish test data")
        {
            Name = "test data",
            Path = "artifacts/test/",
            If = "always()"
        });

        buildJob.Steps.Add(new UploadArtifactStep("Publish NuGet Packages")
        {
            Name = "nuget",
            Path = "artifacts/nuget/",
            If = "always()"
        });

        return configuration;
    }
}