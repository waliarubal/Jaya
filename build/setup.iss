; process command line arguments
#ifndef APP_NAME
	#define APP_NAME "Jaya File Manager"
#endif
#ifndef APP_VERSION
	#define APP_VERSION "1.0.0.0"
#endif
#ifndef APP_ROOT
	#define APP_ROOT "..\"
#endif

#define APP_PUBLISHER "Rubal Walia"
#define APP_EMAIL "walia.rubal@gmail.com"
#define APP_URL "https://github.com/waliarubal/Jaya"
#define APP_EXECUTABLE "Jaya.Ui.exe"
#define APP_ID "{395A0915-9AD7-4CB5-A72B-3369DF5656E4}"

[Setup]
AppId={{#APP_ID}
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
LicenseFile={#APP_ROOT}\LICENSE
PrivilegesRequiredOverridesAllowed=dialog
OutputBaseFilename=windows
SetupIconFile={#APP_ROOT}\src\Jaya.Ui\Assets\Logo.ico
UninstallDisplayName={#APP_NAME}
UninstallDisplayIcon={app}\{#APP_EXECUTABLE}
Compression=lzma
SolidCompression=yes
WizardStyle=classic
SetupLogging=yes
UsePreviousAppDir=yes

[INI]
Filename: "config.ini"; Section: "InstallSettings"; Flags: uninsdeletesection
Filename: "config.ini"; Section: "InstallSettings"; Key: "InstallPath"; String: "{app}"

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 6.1; Check: not IsAdminInstallMode

[Files]
Source: "{#APP_ROOT}\publish\windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

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
  sUnInstPath := ExpandConstant('SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{{#APP_ID}');
  if (IsWin64()) then begin
    RegQueryStringValue(HKLM64, sUnInstPath, 'UninstallString', sUnInstallString);
    Result := sUnInstallString;
  end
  else begin
    RegQueryStringValue(HKLM32, sUnInstPath, 'UninstallString', sUnInstallString);
    Result := sUnInstallString;
  end;
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
    if ShellExec('', 'msiexec',  '/uninstall {#APP_ID} /quiet', '', SW_SHOWNORMAL, ewWaitUntilTerminated, iResultCode) then // for ver 1.4.4 -> 1.4.5
      Result := 3
    else
      Result := 2;
  end else
    Result := 1;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  case CurStep of
    ssInstall:
      begin
        if (IsUpgrade()) then
        begin
          UnInstallOldVersion();
        end;
      end;

     ssPostInstall:
     begin
      
     end;
  end;

  if (CurStep=ssInstall) then
  begin
    if (IsUpgrade()) then
    begin
      UnInstallOldVersion();
    end;
  end;
end;

function InitializeSetup(): boolean;
begin
  if (IsUpgrade()) then
  begin
    MsgBox(ExpandConstant('{cm:RemoveOld}'), mbInformation, MB_OK);
    UnInstallOldVersion();
  end;
	Result := true;
end;