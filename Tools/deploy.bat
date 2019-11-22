@echo off

SET 7Z=%APPVEYOR_BUILD_FOLDER%/Tools/7za.exe

echo Switch to build directory   
cd %APPVEYOR_BUILD_FOLDER%

echo    
echo Build for Windows (64-bit)    
SET OUTPUT=%APPVEYOR_BUILD_FOLDER%/windows_%BUILD_VERSION%.zip    
SET OUTPUT_DIR=%APPVEYOR_BUILD_FOLDER%/windows
dotnet publish -c Release -r win-x64 --self-contained true --output %OUTPUT_DIR%   
%7Z% a -tzip %OUTPUT% %OUTPUT_DIR%/*

echo    
echo Build for Linux (64-bit)   
SET OUTPUT=%APPVEYOR_BUILD_FOLDER%/linux_%BUILD_VERSION%.zip    
SET OUTPUT_DIR=%APPVEYOR_BUILD_FOLDER%/linux
dotnet publish -c Release -r linux-x64 --self-contained true --output %OUTPUT_DIR%
%7Z% a -tzip %OUTPUT% %OUTPUT_DIR%/*

echo    
echo Build for Mac OS (64-bit)    
SET OUTPUT=%APPVEYOR_BUILD_FOLDER%/osx_%BUILD_VERSION%.zip    
SET OUTPUT_DIR=%APPVEYOR_BUILD_FOLDER%/osx
dotnet publish -c Release -r osx-x64 --self-contained true --output %OUTPUT_DIR%    
%7Z% a -tzip %OUTPUT% %OUTPUT_DIR%/*