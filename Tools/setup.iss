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
SetupMutex={#APP_NAME}
UsePreviousAppDir=yes
CreateUninstallRegKey=no
UpdateUninstallLogAppName=no

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