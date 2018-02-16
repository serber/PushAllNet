param(
	[Parameter (Mandatory=$true)]
	[string] $BuildNumber,
	
	[Parameter (Mandatory=$true)]
	[string] $apikey
	)
	
function DoBuild {
	# Set default ErrorAction to Stop for all commands
	$ErrorActionPreference="Stop"
		
	Write-Host "(1/8) Removing bin folders"
		Get-ChildItem -Include bin -Recurse -Force | Remove-Item -Force -Recurse

	Write-Host "(2/8) Removing obj folders"
		Get-ChildItem -Include obj -Recurse -Force | Remove-Item -Force -Recurse

	Write-Host "(3/8) Removing paket-files folder"
	if (Test-Path -Path .\paket-files\) { 
		Remove-Item -Recurse -Force .\paket-files\
	}
		
	Write-Host "(4/8) Removing output folder"
	if (Test-Path -Path .\nuget\) { 
		Remove-Item -Recurse -Force .\nuget\
	}	
	
    Write-Host "(5/8) Install dotnet"
	if ([bool](Get-Command -Name dotnet -ErrorAction SilentlyContinue))
	{
		Write-Host "(already installed)"
	}
	else
	{
		.\dotnet-install.ps1
	}
	
	Write-Host "(6/8) Build solution"
		$nugetVersion = "1.0";
		$version = $nugetVersion + ".$BuildNumber"
		dotnet restore
		dotnet pack -o $pwd\nuget -c Release /p:PackageVersion=$version --include-symbols
		
	Write-Host "(7/8) Push packets"
		dotnet nuget push .\nuget\*.symbols.nupkg -t 900 -k $apikey -s https://api.nuget.org/v3/index.json
		
	Write-Host "(8/8) Clean"
	    Get-ChildItem -Include bin -Recurse -Force | Remove-Item -Force -Recurse
		Get-ChildItem -Include obj -Recurse -Force | Remove-Item -Force -Recurse
				
		if (Test-Path -Path .\nuget\) { 
		Remove-Item -Recurse -Force .\nuget\
	    }
		
	Write-Host "Done"
}

[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
DoBuild("")