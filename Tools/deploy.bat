@ECHO off

SET SEVEN_ZIP=%APPVEYOR_BUILD_FOLDER%/Tools/7z/7za.exe
SET CURL=%APPVEYOR_BUILD_FOLDER%/Tools/curl/curl.exe

ECHO Switch to build directory   
CD %APPVEYOR_BUILD_FOLDER%

ECHO;
ECHO Build for Windows (64-bit)    
SET OUTPUT=%APPVEYOR_BUILD_FOLDER%/windows_%BUILD_VERSION%.zip    
SET OUTPUT_DIR=%APPVEYOR_BUILD_FOLDER%/windows
SET PARAM=a -tzip %OUTPUT% %OUTPUT_DIR%/*
CALL dotnet publish -c Release -r win-x64 --self-contained true --output %OUTPUT_DIR%   
START /wait /B "" %SEVEN_ZIP% %PARAM%

ECHO;   
ECHO Build for Linux (64-bit)   
SET OUTPUT=%APPVEYOR_BUILD_FOLDER%/linux_%BUILD_VERSION%.zip    
SET OUTPUT_DIR=%APPVEYOR_BUILD_FOLDER%/linux
SET PARAM=a -tzip %OUTPUT% %OUTPUT_DIR%/*
CALL dotnet publish -c Release -r linux-x64 --self-contained true --output %OUTPUT_DIR%
START /wait /B "" %SEVEN_ZIP% %PARAM%

ECHO;   
ECHO Build for Mac OS (64-bit)    
SET OUTPUT=%APPVEYOR_BUILD_FOLDER%/osx_%BUILD_VERSION%.zip    
SET OUTPUT_DIR=%APPVEYOR_BUILD_FOLDER%/osx
SET PARAM=a -tzip %OUTPUT% %OUTPUT_DIR%/*
CALL dotnet publish -c Release -r osx-x64 --self-contained true --output %OUTPUT_DIR%    
START /wait /B "" %SEVEN_ZIP% %PARAM%