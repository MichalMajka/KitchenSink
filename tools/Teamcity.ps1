Param ($checkoutdir, $nunitversion, $browsersToRun)

$StarCounterDir = "$checkoutdir\sc"
$StarCounterWorkDirPath = "$StarCounterDir\starcounter-workdir"
$StarCounterRepoPath = "$StarCounterWorkDirPath\personal"
$StarCounterConfigPath = "$StarCounterDir\Configuration"

$KitchenSinkWwwPath = "$checkoutdir\KitchenSink\src\KitchenSink\wwwroot"
$KitchenSinkExePath = "$checkoutdir\KitchenSink\bin\Debug\KitchenSink.exe"
$KitchenSinkTestsPath = "$checkoutdir\KitchenSink\test\KitchenSink.Tests\bin\Debug\KitchenSink.Tests.dll"
$KitchenSinkArg = "--resourcedir=$KitchenSinkWwwPath $KitchenSinkExePath"

$NunitConsoleRunnerExePath = "$checkoutdir\KitchenSink\packages\NUnit.ConsoleRunner.$nunitversion\tools\nunit3-console.exe"
$NunitArg = "$KitchenSinkTestsPath --noheader --teamcity --params Browsers=$browsersToRun"

$StarExePath = "$StarCounterDir\star.exe"
$StarAdminExePath = "$StarCounterDir\staradmin.exe"

Function createXML($repoPath, $configPath)
{
	$fileContent = "<?xml version=`"1.0`" encoding=`"UTF-8`"?>
<service><server-dir>$repoPath</server-dir></service>"
	
	New-Item -Path $configPath -Name personal.xml -ItemType "file" -force -Value $fileContent | Out-Null
	return Test-Path $configPath\personal.xml
}

try 
{
	$createRepo = Start-Process -FilePath $StarExePath -ArgumentList "`@`@createrepo $StarCounterWorkDirPath" -PassThru -NoNewWindow -Wait
	if ($createRepo.ExitCode -eq 0)
	{
		$createXMLExitCode = createXML -repoPath $StarCounterRepoPath -configPath $StarCounterConfigPath
		if ($createXMLExitCode)
		{ 
			$KitchenSink = Start-Process -FilePath $StarExePath -ArgumentList $KitchenSinkArg -PassThru -NoNewWindow
			wait-process -id $KitchenSink.Id
			$Tests = Start-Process -FilePath $NunitConsoleRunnerExePath -ArgumentList $NunitArg -PassThru -NoNewWindow -Wait
			if($Tests.ExitCode -ge 0)
			{
				$KillStarcounter = Start-Process -FilePath $StarAdminExePath -ArgumentList "kill all" -PassThru -NoNewWindow -Wait
				if($KillStarcounter.ExitCode -eq 0) { exit(0) }
				else { exit(1) }
			}
			else { exit(1) }
		}
		else { exit(1) }
	}
	else { exit(1) }
} 
Catch 
{
	$ErrorMessage = $_.Exception.Message
	Write-Output $ErrorMessage
	exit(1)
}