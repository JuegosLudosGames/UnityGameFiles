; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Gift Project"
#define MyAppVersion "0.1.5"
#define MyAppPublisher "JuegosLudosGames"
#define MyAppExeName "Gift Project.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{45DEAD8A-F503-41BE-A2F3-E452FEE0F5C8}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={pf}\{#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=C:\Users\james\Documents\Unity Projects\Gift Project\_Builds\Standalone Windows Pc\Build 5\LICENSE.txt
InfoBeforeFile=C:\Users\james\Documents\Unity Projects\Gift Project\_Builds\Standalone Windows Pc\Build 5\README.txt
OutputDir=C:\Users\james\Documents\Unity Projects\Gift Project\_Builds\Installer config\_Installations\0.1.5
OutputBaseFilename=Gift-Project-Installer-fix
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\james\Documents\Unity Projects\Gift Project\_Builds\Standalone Windows Pc\Build 5\Gift Project.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\james\Documents\Unity Projects\Gift Project\_Builds\Standalone Windows Pc\Build 5\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{commonprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
