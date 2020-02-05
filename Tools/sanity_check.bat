@ECHO off
ECHO Build Version: %BUILD_VERSION%
ECHO Build Directory: %APPVEYOR_BUILD_FOLDER%
CALL dotnet build --configuration Release