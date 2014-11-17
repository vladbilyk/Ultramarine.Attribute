properties {
    $currentDir = resolve-path .
    $pester = "$currentDir\packages\Pester.3.1.1\tools\bin\pester.bat"
}

Task Default -depends Compile

Task Compile {
    msbuild "..\Source\Ultramarine.Attribute\Ultramarine.Attribute.sln"
}

Task Test {
    CD ..\Source\Ultramarine.Attribute.Powershell\Tests 
    exec { . "$pester" }
    CD $currentDir
}