Import-Module ..\bin\Release\Ultramarine.Attribute.Powershell.dll

Describe "Cmdlet tests" {
    Context "Tests context" {

        It "Hello" {
            Get-Attribute | Should Be "Hello from cmdlet!"
        }
    }
}