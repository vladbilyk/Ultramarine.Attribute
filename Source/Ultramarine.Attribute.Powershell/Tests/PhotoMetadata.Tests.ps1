Import-Module ..\bin\Release\Ultramarine.Attribute.Powershell.dll

Describe "PhotoMetadata provider tests" {

    Context "ChildItems" {
        $imagePath = Join-Path $PSScriptRoot dsc_4646.jpg
        New-PSDrive -Name img -PSProvider PhotoMetadata -Root $imagePath

        It "Root" {
            Get-ChildItem img:\ | Should Be "System"
        }

        It "System" {
            (Get-ChildItem img:\System -Name).Count | Should Be 13
        }

        It "System.GPS" {
            (Get-ChildItem img:\System.GPS -Name).Count | Should Be 30
        }

        It "System.Image" {
            (Get-ChildItem img:\System.Image -Name).Count | Should Be 7
        }

        It "System.Photo" {
            (Get-ChildItem img:\System.Photo -Name).Count | Should Be 36
        }
    }

    Context "Items" {
        $imagePath = Join-Path $PSScriptRoot dsc_4646.jpg
        New-PSDrive -Name image -PSProvider PhotoMetadata -Root $imagePath

        It "Camera Model" {
            Get-Item image:\System.Photo.CameraModel | Should Be "NIKON D300"
        }

        It "Camera Manufacturer" {
            Get-Item image:\System.Photo.CameraManufacturer | Should Be "NIKON CORPORATION"
        }

        It "Comments" {
            Get-Item image:\System.Comment | Should Be "VL@D BILYK.       +7(906)2473265"
        }

        It "Program Name" {
            Get-Item image:\System.ApplicationName | Should Be "Adobe Photoshop Lightroom 3.5 (Windows)"
        }

        It "Date Taken" {
            (Get-Item image:\System.Photo.DateTaken).Day | Should Be 26
        }

        It "Vertical Resolution" {
            Get-Item image:\System.Image.VerticalResolution | Should Be 300
        }

        It "Horizontal Resolution" {
            Get-Item image:\System.Image.VerticalResolution | Should Be 300
        }

        It "ResolutionUnit" {
            Get-Item image:\System.Image.ResolutionUnit | Should Be "Inch"
        }

        It "Aperture" {
            Get-Item image:\System.Photo.Aperture | Should Be 4
        }

        It "Exposure time" {
            Get-Item image:\System.Photo.ExposureTime | Should Be (1.0 / 800)
        }
    }
}