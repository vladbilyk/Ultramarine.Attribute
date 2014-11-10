Import-Module Ultramarine.Attribute.Powershell
if (!(Test-Path "image:"))
{
	$imagePath = Join-Path $PSScriptRoot dsc_4646.jpg
	New-PSDrive -Name image -PSProvider PhotoMetadata -Root $imagePath
}
Get-Item image:\System.Photo.CameraModel
Get-Item image:\System.Photo.CameraManufacturer
Remove-PSDrive image