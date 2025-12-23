# Quick coverage run with console summary only
Write-Host "Running tests with code coverage..." -ForegroundColor Green

if (Test-Path ".\TestResults") {
    Remove-Item -Recurse -Force ".\TestResults"
}
New-Item -ItemType Directory -Path ".\TestResults" -Force | Out-Null

# Run unit tests with migrations excluded
Set-Location ".\tests\EMA.Tests.Unit"
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput="..\..\TestResults\coverage.unit.xml" /p:ExcludeByFile="**\Migrations\*.cs"
Set-Location "..\..\"

# Run integration tests with migrations excluded
Set-Location ".\tests\EMA.Tests.Integration"
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput="..\..\TestResults\coverage.integration.xml" /p:ExcludeByFile="**\Migrations\*.cs"
Set-Location "..\..\"

Write-Host "`nGenerating coverage summary..." -ForegroundColor Green
reportgenerator `
    -reports:".\TestResults\*.xml" `
    -targetdir:".\CoverageReport" `
    -reporttypes:"TextSummary"

Write-Host "`nCoverage Summary:" -ForegroundColor Cyan
Get-Content ".\CoverageReport\Summary.txt"
