Import-Module ..\bin\Release\Ultramarine.Attribute.Powershell.dll

Describe "Cmdlet tests" {
    Context "Tests context" {

        It "Path is required" {
            { Get-Attribute } | Should Throw "Cannot process command because of one or more missing mandatory parameters: Path."
        }

        It "Dummy check of Path parameter" {
            Get-Attribute -Path "Hello!" | Should Be "Hello!"
        }

    }
}