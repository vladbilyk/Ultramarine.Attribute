Import-Module ..\bin\Release\Ultramarine.Attribute.Powershell.dll
New-PSDrive -Name img -PSProvider PhotoMetadata -Root dsc_4646.jpg -Verbose
Get-ChildItem img:\System.GPS -Verbose
