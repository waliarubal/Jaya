Set-Location ./Jaya.app/Contents
if (-not (Test-Path ./MacOS)) {
    New-Item MacOS -ItemType Directory | Out-Null
}
Set-Location ./MacOS
Remove-Item *
Set-Location ../../../
Set-Location ./Jaya.Ui
dotnet publish -r osx.10.14-x64 -o ../Jaya.app/Contents/MacOS -p:PublishSingleFile=true
Set-Location ..
