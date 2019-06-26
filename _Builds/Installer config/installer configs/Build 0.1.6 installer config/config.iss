; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Gift-Project-Test"
#define MyAppVersion "0.6"
#define MyAppPublisher "JugeosLudosGames"
#define MyAppExeName "Gift Project.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{A577A515-BE09-4C76-A8E8-A78F71474004}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={pf}\Gift-Project
DisableProgramGroupPage=yes
LicenseFile=C:\Users\james\Documents\Unity Projects\Gift Project\_Builds\Standalone Windows Pc\Build 0.6\LICENSE.txt
InfoBeforeFile=C:\Users\james\Documents\Unity Projects\Gift Project\_Builds\Standalone Windows Pc\Build 0.6\README.txt
OutputDir=C:\Users\james\Documents\Unity Projects\Gift Project\_Builds\Installer config\_Installations\0.1.6
OutputBaseFilename=Gift-Project-Installer x86_64
Compression=lzma
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\james\Documents\Unity Projects\Gift Project\_Builds\Standalone Windows Pc\Build 0.6\Gift Project.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\james\Documents\Unity Projects\Gift Project\_Builds\Standalone Windows Pc\Build 0.6\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{commonprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
