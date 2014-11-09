Import-Module Ultramarine.Attribute.Powershell
$imageDrive = Get-PSDrive -Name image
if (!$imageDrive)
{
	New-PSDrive -Name image -PSProvider PhotoMetadata -Root "C:\Users\vbilyk\Pictures\Photo Stream\My Photo Stream\IMG_0022.JPG"
}
Get-Item image:\System.Photo.CameraModel
Get-Item image:\System.Photo.CameraManufacturer
Remove-PSDrive image