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

ECHO;
ECHO Create GitHub release
SET RELEASE={\"tag_name\": \"v%BUILD_VERSION%\", \"target_commitish\": \"dev\", \"name\": \"v%BUILD_VERSION%\", \"body\": \"\", \"draft\": false, \"prerelease\": true}
SET PARAM=--data "%RELEASE%" --header "Content-Type: application/json" --request POST https://api.github.com/repos/waliarubal/jaya/releases
START /wait /B "" %CURL% %PARAM%

ECHO;
ECHO Upload Windows binaries
SET OUTPUT=%APPVEYOR_BUILD_FOLDER%/windows_%BUILD_VERSION%.zip    
SET PARAM=-h 'Content-Type: application/octet-stream' --upload-file @%OUTPUT% https://uploads.github.com/repos/waliarubal/jaya/releases/v%BUILD_VERSION%/assets?name=windows.zip
START /wait /B "" %CURL% %PARAM%

ECHO;
ECHO Upload Linux binaries
SET OUTPUT=%APPVEYOR_BUILD_FOLDER%/linux_%BUILD_VERSION%.zip    
SET PARAM=-h 'Content-Type: application/octet-stream' --upload-file @%OUTPUT% https://uploads.github.com/repos/waliarubal/jaya/releases/v%BUILD_VERSION%/assets?name=linux.zip
START /wait /B "" %CURL% %PARAM%

ECHO;
ECHO Upload Mac OS binaries
SET OUTPUT=%APPVEYOR_BUILD_FOLDER%/osx_%BUILD_VERSION%.zip    
SET PARAM=-h 'Content-Type:application/octet-stream' --upload-file @%OUTPUT% https://uploads.github.com/repos/waliarubal/jaya/releases/v%BUILD_VERSION%/assets?name=osx.zip
START /wait /B "" %CURL% %PARAM%