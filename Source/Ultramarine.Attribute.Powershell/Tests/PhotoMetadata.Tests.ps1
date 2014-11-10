Import-Module Ultramarine.Attribute.Powershell

Describe "PhotoMetadata provider tests" {
    Context "Image drive" {
        $imagePath = Join-Path $PSScriptRoot dsc_4646.jpg
        New-PSDrive -Name image -PSProvider PhotoMetadata -Root $imagePath

        It "Camera Model" {
            Get-Item image:\System.Photo.CameraModel | Should Be "NIKON D300"
        }

        It "Camera Manufacturer" {
            Get-Item image:\System.Photo.CameraManufacturer | Should Be "NIKON CORPORATION"
        }
    }
}