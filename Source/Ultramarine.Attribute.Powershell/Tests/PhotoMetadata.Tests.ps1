﻿Import-Module ..\bin\Release\Ultramarine.Attribute.Powershell.dll

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

        It "Comments" {
            Get-Item image:\System.Comment | Should Be "VL@D BILYK.       +7(906)2473265"
        }

        It "Program Name" {
            Get-Item image:\System.ApplicationName | Should Be "Adobe Photoshop Lightroom 3.5 (Windows)"
        }

        It "Date Taken" {
            (Get-Item image:\System.Photo.DateTaken).Day | Should Be 26
        }

    }
}