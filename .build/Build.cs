using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using Rocket.Surgery.Nuke;
using Rocket.Surgery.Nuke.DotNetCore;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
class EverCraft : NukeBuild,
    ICanRestoreWithDotNetCore,
    ICanBuildWithDotNetCore,
    ICanTestWithDotNetCore,
    ICanPackWithDotNetCore,
    IHaveConfiguration<Configuration>
    
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<EverCraft>(x => x.Default);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    public Configuration Configuration { get; } = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    [ComputedGitVersion] public GitVersion GitVersion { get; } = null;

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    public bool CollectCoverage { get; } = false;

    private Target Default => _ => _
        .DependsOn(Restore)
        .DependsOn(Build)
        .DependsOn(Test)
        .DependsOn(Pack);

    public Target Clean => _ => _.Inherit<ICanClean>(x => x.Clean);

    public Target Restore => _ => _.Inherit<ICanRestoreWithDotNetCore>(x => x.CoreRestore);

    public Target Test => _ => _.Inherit<ICanTestWithDotNetCore>(x => x.CoreTest);

    public Target Build => _ => _.Inherit<ICanBuildWithDotNetCore>(x => x.CoreBuild);

    public Target Pack => _ => _.Inherit<ICanPackWithDotNetCore>(x => x.CorePack);
}
