; process command line arguments
#ifndef APP_VERSION
	#define APP_VERSION "1.0"
#endif
#ifndef APP_ROOT
	#define APP_ROOT ".."
#endif

#define APP_NAME "Jaya - Cross Plat"
#define APP_PUBLISHER "Rubal Walia"
#define APP_EMAIL "walia.rubal@gmail.com"
#define APP_URL "https://github.com/waliarubal/Jaya"
#define APP_EXECUTABLE "Jaya.Ui.exe"

[Setup]
AppId={{395A0915-9AD7-4CB5-A72B-3369DF5656E4}
AppName={#APP_NAME}
AppVersion={#APP_VERSION}
AppContact={#APP_EMAIL}
AppPublisher={#APP_PUBLISHER}
AppPublisherURL={#APP_URL}
AppSupportURL={#APP_URL}
AppUpdatesURL={#APP_URL}
DefaultDirName={autopf}\Jaya
DisableProgramGroupPage=yes
UsedUserAreasWarning=no
LicenseFile={#APP_ROOT}LICENSE
PrivilegesRequiredOverridesAllowed=dialog
OutputBaseFilename=windows
SetupIconFile={#APP_ROOT}Jaya.Ui\Assets\Logo.ico
UninstallDisplayName={#APP_NAME}
UninstallDisplayIcon={app}\{#APP_EXECUTABLE}
Compression=lzma
SolidCompression=yes
WizardStyle=classic
SetupLogging=yes
UsePreviousAppDir=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 6.1; Check: not IsAdminInstallMode

[Files]
Source: "{#APP_ROOT}publish\windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{autoprograms}\{#APP_NAME}"; Filename: "{app}\{#APP_EXECUTABLE}"
Name: "{autodesktop}\{#APP_NAME}"; Filename: "{app}\{#APP_EXECUTABLE}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#APP_NAME}"; Filename: "{app}\{#APP_EXECUTABLE}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#APP_EXECUTABLE}"; Description: "{cm:LaunchProgram,{#StringChange(APP_NAME, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Code]
function GetUninstallString(): String;
var
  sUnInstPath: String;
  sUnInstallString: String;
begin
  sUnInstPath := ExpandConstant('Software\Microsoft\Windows\CurrentVersion\Uninstall\{395A0915-9AD7-4CB5-A72B-3369DF5656E4}_is1');
  sUnInstallString := '';
  if not RegQueryStringValue(HKLM, sUnInstPath, 'UninstallString', sUnInstallString) then
    RegQueryStringValue(HKCU, sUnInstPath, 'UninstallString', sUnInstallString);
  Result := sUnInstallString;
end;

function IsUpgrade(): Boolean;
begin
  Result := (GetUninstallString() <> '');
end;

function UnInstallOldVersion(): Integer;
var
  sUnInstallString: String;
  iResultCode: Integer;
begin
// Return Values:
// 1 - uninstall string is empty
// 2 - error executing the UnInstallString
// 3 - successfully executed the UnInstallString

  // default return value
  Result := 0;

  // get the uninstall string of the old app
  sUnInstallString := GetUninstallString();
  if sUnInstallString <> '' then begin
    sUnInstallString := RemoveQuotes(sUnInstallString);
    if Exec(sUnInstallString, '/SILENT /NORESTART /SUPPRESSMSGBOXES','', SW_HIDE, ewWaitUntilTerminated, iResultCode) then
      Result := 3
    else
      Result := 2;
  end else
    Result := 1;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if (CurStep=ssInstall) then
  begin
    if (IsUpgrade()) then
    begin
      UnInstallOldVersion();
    end;
  end;
end;