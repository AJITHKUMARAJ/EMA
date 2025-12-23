# Run tests with code coverage collection
Write-Host "Running tests with code coverage..." -ForegroundColor Green

# Clean previous coverage results
if (Test-Path ".\TestResults") {
    Remove-Item -Recurse -Force ".\TestResults"
}
if (Test-Path ".\CoverageReport") {
    Remove-Item -Recurse -Force ".\CoverageReport"
}

# Create directory for coverage results
New-Item -ItemType Directory -Path ".\TestResults" -Force | Out-Null

# Run unit tests with migrations excluded
Write-Host "Running unit tests..." -ForegroundColor Yellow
Set-Location ".\tests\EMA.Tests.Unit"
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput="..\..\TestResults\coverage.unit.xml" /p:ExcludeByFile="**\Migrations\*.cs"
Set-Location "..\..\"

# Run integration tests with migrations excluded
Write-Host "Running integration tests..." -ForegroundColor Yellow
Set-Location ".\tests\EMA.Tests.Integration"
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput="..\..\TestResults\coverage.integration.xml" /p:ExcludeByFile="**\Migrations\*.cs"
Set-Location "..\..\"

# Generate HTML report from both coverage files
Write-Host "`nGenerating HTML coverage report..." -ForegroundColor Green
reportgenerator `
    -reports:".\TestResults\*.xml" `
    -targetdir:".\CoverageReport" `
    -reporttypes:"Html;HtmlSummary;Badges;TextSummary"

# Display summary
Write-Host "`nCoverage Summary:" -ForegroundColor Cyan
Get-Content ".\CoverageReport\Summary.txt"

# Open HTML report
Write-Host "`nOpening coverage report in browser..." -ForegroundColor Green
Start-Process ".\CoverageReport\index.html"

Write-Host "`nCoverage report location: $PWD\CoverageReport\index.html" -ForegroundColor Yellow
