Import-Module ..\bin\Release\Ultramarine.Attribute.Powershell.dll

Describe "Cmdlet tests" {
    Context "Tests context" {

        It "Path is required" {
            { Get-Attribute } | Should Throw "Cannot process command because of one or more missing mandatory parameters: Path."
        }

        It "Simple check of Path parameter" {
            { Get-Attribute -Path "Hello!" } | Should Throw "Cannot find path"
        }

        It "All attributes" {
            (Get-Attribute -Path "dsc_4646.jpg").Count | Should Be 83
        }

        It "Date taken" {
            (Get-Attribute -Path "dsc_4646.jpg")["System.Photo.DateTaken"].Day | Should Be 26 
        }

        It "Resolution Unit" {
             (Get-Attribute -Path "dsc_4646.jpg")["System.Image.ResolutionUnit"] | Should Be "Inch"
        }

    }
}