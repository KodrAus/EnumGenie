echo "build: Hacking strong name stuff"

reg ADD "HKLM\Software\Microsoft\StrongName\Verification\*,31bf3856ad364e35" /f
if ($env:PROCESSOR_ARCHITECTURE -eq "AMD64")
{
    reg ADD "HKLM\Software\Wow6432Node\Microsoft\StrongName\Verification\*,31bf3856ad364e35" /f
}

echo "build: Build $env:APPVEYOR_BUILD_VERSION started"

Push-Location $PSScriptRoot

& dotnet restore --no-cache

$branch = @{ $true = $env:APPVEYOR_REPO_BRANCH; $false = $(git symbolic-ref --short -q HEAD) }[$env:APPVEYOR_REPO_BRANCH -ne $NULL];
$revision = @{ $true = "{0:00000}" -f [convert]::ToInt32("0" + $env:APPVEYOR_BUILD_NUMBER, 10); $false = "local" }[$env:APPVEYOR_BUILD_NUMBER -ne $NULL];
$suffix = @{ $true = ""; $false = "$($branch.Substring(0, [math]::Min(10,$branch.Length)))-$revision"}[$branch -eq "master" -and $revision -ne "local"]
$commitHash = $(git rev-parse --short HEAD)
$buildSuffix = @{ $true = "$($suffix)-$($commitHash)"; $false = "$($branch)-$($commitHash)" }[$suffix -ne ""]
$buildVersion = @{ $true = $env:APPVEYOR_BUILD_VERSION; $false = "0.0.1" }[$env:APPVEYOR_BUILD_VERSION -ne $NULL]

$fullBuildVersion = "$buildVersion-$buildSuffix"

echo "build: Package version suffix is $suffix"
echo "build: Build version is $buildVersion"
echo "build: Build version suffix is $buildSuffix" 

& msbuild /t:Pack /p:Configuration=Release EnumGenie.Core/EnumGenie.Core.csproj /p:PackageVersion=$fullBuildVersion
& msbuild /t:Pack /p:Configuration=Release EnumGenie.TypeScript/EnumGenie.TypeScript.csproj /p:PackageVersion=$fullBuildVersion

Pop-Location