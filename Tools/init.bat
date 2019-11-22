@ECHO off
FOR /f %%a IN ('powershell -Command "Get-Date -format yy.MM.dd.HHmm"') do set BUILD_VERSION=%%a