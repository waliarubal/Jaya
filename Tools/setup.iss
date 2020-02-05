#define APP_NAME "Jaya - Cross Plat"
#define APP_VERSION "1.0"
#define APP_PUBLISHER "Rubal Walia"
#define APP_URL "https://github.com/waliarubal/Jaya"
#define APP_EXECUTABLE "MyProg.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{395A0915-9AD7-4CB5-A72B-3369DF5656E4}
AppName={#APP_NAME}
AppVersion={#APP_VERSION}
AppPublisher={#APP_PUBLISHER}
AppPublisherURL={#APP_URL}
AppSupportURL={#APP_URL}
AppUpdatesURL={#APP_URL}
DefaultDirName={autopf}\Jaya
DisableProgramGroupPage=yes
; The [Icons] "quicklaunchicon" entry uses {userappdata} but its [Tasks] entry has a proper IsAdminInstallMode Check.
UsedUserAreasWarning=no
LicenseFile=C:\Program Files (x86)\Inno Setup 6\license.txt
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
PrivilegesRequiredOverridesAllowed=dialog
OutputBaseFilename=windows
SetupIconFile=F:\Projects\Jaya\Jaya.Ui\Assets\Logo.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 6.1; Check: not IsAdminInstallMode

[Files]
Source: "C:\Program Files (x86)\Inno Setup 6\Examples\MyProg.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\admin\Desktop\Android SDK_20180704\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#APP_NAME}"; Filename: "{app}\{#APP_EXECUTABLE}"
Name: "{autodesktop}\{#APP_NAME}"; Filename: "{app}\{#APP_EXECUTABLE}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#APP_NAME}"; Filename: "{app}\{#APP_EXECUTABLE}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#APP_EXECUTABLE}"; Description: "{cm:LaunchProgram,{#StringChange(APP_NAME, '&', '&&')}}"; Flags: nowait postinstall skipifsilent