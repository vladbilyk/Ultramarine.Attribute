Import-Module Ultramarine.Attribute.Powershell
$imageDrive = Get-PSDrive -Name image
if (!$imageDrive)
{
	$imagePath = Join-Path $PSScriptRoot dsc_4646.jpg
	New-PSDrive -Name image -PSProvider PhotoMetadata -Root $imagePath
}
Get-Item image:\System.Photo.CameraModel
Get-Item image:\System.Photo.CameraManufacturer
Remove-PSDrive image