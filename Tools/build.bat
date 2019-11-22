@ECHO off
ECHO Build Version: %BUILD_VERSION%
ECHO Build Directory: %APPVEYOR_BUILD_FOLDER%
EXEC dotnet build --configuration Release