@echo off
echo Build Version: %BUILD_VERSION%
echo Build Directory: %APPVEYOR_BUILD_FOLDER%
dotnet build --configuration Release