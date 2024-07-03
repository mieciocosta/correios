# RunTests.ps1

# Set the output encoding to UTF-8
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

# Run the tests and save the log
$results = dotnet test --logger "console;verbosity=detailed" | Tee-Object -FilePath test.log

# Display the content of test.log for debugging
Write-Host "Content of test.log:" -ForegroundColor Yellow
Get-Content test.log | Write-Host

# Extract and format test results
$testResults = @()

# Collecting success results
Write-Host "Collecting success results..." -ForegroundColor Yellow
$successResults = Select-String -Path test.log -Pattern "Aprovado" -Context 0,5
foreach ($result in $successResults) {
    $testResults += [PSCustomObject]@{
        Status = "Aprovado"
        Test = $result.Line -replace ".*Aprovado (.+) \[.*\]", '$1'
        Time = $result.Line -replace ".*\[(.+)\]", '$1'
        Details = ($result.Context.DisplayPostContext -join "`n") -replace "\`[.*?\`]", ""
    }
}

# Collecting failure results
Write-Host "Collecting failure results..." -ForegroundColor Yellow
$failureResults = Select-String -Path test.log -Pattern "Com falha" -Context 0,5
foreach ($result in $failureResults) {
    $testResults += [PSCustomObject]@{
        Status = "Com falha"
        Test = $result.Line -replace ".*Com falha (.+) \[.*\]", '$1'
        Time = $result.Line -replace ".*\[(.+)\]", '$1'
        Details = ($result.Context.DisplayPostContext -join "`n") -replace "\`[.*?\`]", ""
    }
}

# Displaying results
Write-Host "Displaying results..." -ForegroundColor Yellow
if ($testResults.Count -gt 0) {
    $testResults | ForEach-Object {
        $statusColor = if ($_.Status -eq "Aprovado") { "Green" } else { "Red" }
        Write-Host "----------------------------------------" -ForegroundColor Yellow
        Write-Host "Status  : " -NoNewline; Write-Host $_.Status -ForegroundColor $statusColor
        Write-Host "Test    : " $_.Test
        Write-Host "Time    : " $_.Time
        Write-Host "Details : " $_.Details
        Write-Host "----------------------------------------" -ForegroundColor Yellow
        Write-Host ""
    }
    
    # Displaying summary table
    Write-Host "Summary Table" -ForegroundColor Yellow
    $testResults | Format-Table -Property Test, Status, Time, Details -AutoSize | Out-String | Write-Host
} else {
    Write-Host "No test results found." -ForegroundColor Red
}
