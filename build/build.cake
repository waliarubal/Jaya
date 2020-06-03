// script arguments
var buildNumber = Argument<int>("BuildNumber", 1);
var versionPrefix = Argument("VersionPrefix", "0.0.0");

// public variables
var versionString = "{0}.{0}";
var isGithubActionsBuild = GithubActions.IsRunningOnGithubActions;

Setup(context => 
{

});

Teardown(context => 
{

});

Task("GenerateVersionString")
    .Does(() => 
{
    versionString = string.Format(versionString, versionPrefix, buildNumber);
    Information($"Version {versionString}");
});

Task("Build")
    .IsDependentOn("GenerateVersionString")
    .Does(() => 
{
    Information($"Build {(isGithubActionsBuild ? "is" : "is not")} running on Github Actions infrastructure.");
});

Task("Deploy")
    .IsDependentOn("Build")
    .Does(() => 
{

});

RunTarget("Deploy");