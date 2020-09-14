dotnet tool install dotnet-reportgenerator-globaltool --version 4.6.1 --tool-path .tools

if (Test-Path .\.testResult\) {
    Remove-Item -r .\.testResult\
}

dotnet test .\tests\App.Testes.Unidade\ --results-directory:".testResult\temp" --collect:"XPlat Code Coverage"
Move-Item .\.testResult\temp\*\coverage.cobertura.xml .\.testResult\unit.coverage.cobertura.xml

dotnet test .\tests\App.Testes.Integrados\ --results-directory:".testResult\temp" --collect:"XPlat Code Coverage"
Move-Item .\.testResult\temp\*\coverage.cobertura.xml .\.testResult\integrated.coverage.cobertura.xml

Remove-Item -r .\.coverageReport\
.\.tools\reportgenerator "-reports:.\.testResult\*.cobertura.xml"  "-targetdir:.coverageReport" "-assemblyfilters:+App.*" "-reporttypes:HtmlInline"

if (Test-Path .\.testResult\) {
    Remove-Item -r .\.testResult\
}
