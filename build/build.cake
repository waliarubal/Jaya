#addin nuget:?package=SharpZipLib
#addin nuget:?package=Cake.Compression

enum OperatingSystem 
{
    Windows,
    MacOS,
    Linux
}

// script arguments and constants
const string APP_NAME = "Jaya File Manager";
var BUILD_NUMBER = Argument<int>("BuildNumber", 1);
var VERSION_PREFIX = Argument("VersionPrefix", "0.0.0");

// public variables
string _versionString = string.Empty;
ConvertableDirectoryPath _sourceDirectory, _buildDirectory, _outputDirectory;
bool _isGithubActionsBuild;
OperatingSystem _operatingSystem;

string GetPath(ConvertableDirectoryPath path)
{
    return MakeAbsolute(path).FullPath;
}

Setup(context => 
{
    _versionString = string.Format("{0}.{1}", VERSION_PREFIX, BUILD_NUMBER);
    _isGithubActionsBuild = GitHubActions.IsRunningOnGitHubActions;

    if (IsRunningOnWindows())
        _operatingSystem = OperatingSystem.Windows;
    else if (IsRunningOnUnix())
        _operatingSystem = OperatingSystem.Linux;
    else
        _operatingSystem = OperatingSystem.MacOS;
     
    _sourceDirectory = Directory("../src");
    _buildDirectory = Directory("../build");
    _outputDirectory = Directory("../publish");
});

Teardown(context => 
{

});

Task("BuildInitialization")
    .Does(() => 
{
    Information($"Build is running on {_operatingSystem} operating system{(_isGithubActionsBuild ? " using Github Actions infrastructure" : string.Empty)}.");
    Information($"Source Directory: {GetPath(_sourceDirectory)}");
    Information($"Build Directory: {GetPath(_buildDirectory)}");
    Information($"Version: {_versionString}");

    Information($"Clean up any existing output in directory '{GetPath(_outputDirectory)}'.");
    CleanDirectories(_outputDirectory);
});

Task("BuildWindows64")
    .WithCriteria(() => _operatingSystem == OperatingSystem.Windows)
    .IsDependentOn("BuildInitialization")
    .Does(() => 
{
    var outputDirectory = _outputDirectory + Directory("windows");

    Information("Build for Windows (64-bit).");
    var settings = new DotNetCorePublishSettings
    {
        Framework = "netcoreapp3.1",
        Configuration = "Release",
        SelfContained = true,
        Runtime = "win-x64",
        OutputDirectory = GetPath(outputDirectory)
    };
    DotNetCorePublish(GetPath(_sourceDirectory), settings);

    Information("Create archive 'windows.zip' from the build.");
    Zip(outputDirectory, _outputDirectory + File("windows.zip"));

    Information("Create installation setup.");
    var setupSettings = new InnoSetupSettings
    {
        OutputDirectory = _outputDirectory,
        EnableOutput = true,
        Defines = new Dictionary<string, string> 
        {
            { "APP_NAME", APP_NAME },
            { "APP_VERSION", _versionString },
            { "APP_ROOT", GetPath(Directory("../")) }
        }
    };
    InnoSetup(_buildDirectory + File("setup.iss"), setupSettings);
});

Task("BuildMacOS64")
    .WithCriteria(() => _operatingSystem == OperatingSystem.MacOS)
    .IsDependentOn("BuildInitialization")
    .Does(() => 
{
    Information("Executing MacOS (64-bit) build.");
});

Task("BuildLinux64")
    .WithCriteria(() => _operatingSystem == OperatingSystem.Linux)
    .IsDependentOn("BuildInitialization")
    .Does(() => 
{
    Information("Executing Linux (64-bit) build.");
});

Task("Deploy")
    .IsDependentOn("BuildWindows64")
    .IsDependentOn("BuildMacOS64")
    .IsDependentOn("BuildLinux64")
    .Does(() => 
{

});

RunTarget("Deploy");