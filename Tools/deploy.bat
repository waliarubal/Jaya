@ECHO off

SET 7Z=%APPVEYOR_BUILD_FOLDER%/Tools/7za.exe

ECHO Switch to build directory   
CD %APPVEYOR_BUILD_FOLDER%

ECHO;
ECHO Build for Windows (64-bit)    
SET OUTPUT=%APPVEYOR_BUILD_FOLDER%/windows_%BUILD_VERSION%.zip    
SET OUTPUT_DIR=%APPVEYOR_BUILD_FOLDER%/windows
CALL dotnet publish -c Release -r win-x64 --self-contained true --output %OUTPUT_DIR%   
CALL %7Z% a -tzip %OUTPUT% %OUTPUT_DIR%/*

ECHO;   
ECHO Build for Linux (64-bit)   
SET OUTPUT=%APPVEYOR_BUILD_FOLDER%/linux_%BUILD_VERSION%.zip    
SET OUTPUT_DIR=%APPVEYOR_BUILD_FOLDER%/linux
CALL dotnet publish -c Release -r linux-x64 --self-contained true --output %OUTPUT_DIR%
CALL %7Z% a -tzip %OUTPUT% %OUTPUT_DIR%/*

ECHO;   
ECHO Build for Mac OS (64-bit)    
SET OUTPUT=%APPVEYOR_BUILD_FOLDER%/osx_%BUILD_VERSION%.zip    
SET OUTPUT_DIR=%APPVEYOR_BUILD_FOLDER%/osx
CALL dotnet publish -c Release -r osx-x64 --self-contained true --output %OUTPUT_DIR%    
CALL %7Z% a -tzip %OUTPUT% %OUTPUT_DIR%/*