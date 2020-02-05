@ECHO off
SETLOCAL EnableDelayedExpansion

IF "%BUILD_VERSION%"=="" SET BUILD_VERSION=1
IF "%APPVEYOR_BUILD_FOLDER%"=="" SET APPVEYOR_BUILD_FOLDER=%~dp0..

SET SEVEN_ZIP=%APPVEYOR_BUILD_FOLDER%/Tools/7z/7za.exe
SET CURL=%APPVEYOR_BUILD_FOLDER%/Tools/curl/curl.exe
SET PUBLISH_FOLDER=%APPVEYOR_BUILD_FOLDER%/publish

ECHO Switch to build directory: %APPVEYOR_BUILD_FOLDER% 
CD %APPVEYOR_BUILD_FOLDER%

ECHO;
ECHO Delete publish folder: %PUBLISH_FOLDER%
DEL /s "%PUBLISH_FOLDER%" /q

ECHO;
ECHO Build for Windows (64-bit)    
SET OUTPUT=%PUBLISH_FOLDER%/windows_%BUILD_VERSION%.zip    
SET OUTPUT_DIR=%PUBLISH_FOLDER%/windows
SET PARAM=a -tzip %OUTPUT% %OUTPUT_DIR%/*
CALL dotnet publish -c Release -r win-x64 --self-contained true --output %OUTPUT_DIR%   
START /wait /B "" %SEVEN_ZIP% %PARAM%

ECHO;   
ECHO Build for Linux (64-bit)   
SET OUTPUT=%PUBLISH_FOLDER%/linux_%BUILD_VERSION%.zip    
SET OUTPUT_DIR=%PUBLISH_FOLDER%/linux
SET PARAM=a -tzip %OUTPUT% %OUTPUT_DIR%/*
CALL dotnet publish -c Release -r linux-x64 --self-contained true --output %OUTPUT_DIR%
START /wait /B "" %SEVEN_ZIP% %PARAM%

ECHO;   
ECHO Build for Mac OS (64-bit)    
SET OUTPUT=%PUBLISH_FOLDER%/osx_%BUILD_VERSION%.zip    
SET OUTPUT_DIR=%PUBLISH_FOLDER%/osx
SET PARAM=a -tzip %OUTPUT% %OUTPUT_DIR%/*
CALL dotnet publish -c Release -r osx-x64 --self-contained true --output %OUTPUT_DIR%    
START /wait /B "" %SEVEN_ZIP% %PARAM%