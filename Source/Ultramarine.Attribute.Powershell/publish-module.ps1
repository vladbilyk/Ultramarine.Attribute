$modulePath = $env:PSModulePath.Split(";")[0]
$ultramarineModule = Join-Path $modulePath "Ultramarine.Attribute.Powershell"
if (!(Test-Path $ultramarineModule))
{
	New-Item -Path $ultramarineModule -ItemType Directory
}
$source = Join-Path $PSScriptRoot ".\bin\Release\*.dll"
Copy-Item -Path $source -Destination $ultramarineModule -Exclude "System.*" -Force 
