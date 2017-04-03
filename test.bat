@ECHO OFF

IF "%CONFIGURATION%"=="" SET CONFIGURATION=Debug

SET INTERACTIVE=1
ECHO %CMDCMDLINE% | find /i "%~0" >nul
IF NOT ERRORLEVEL 1 SET INTERACTIVE=0

:: Stop all the running apps
staradmin stop db default

:: Start the tested app
star --resourcedir="%~dp0src\KitchenSink\wwwroot" "%~dp0bin/%CONFIGURATION%/KitchenSink.exe"
IF ERRORLEVEL 1 EXIT /B 1

:: Start the test
IF NOT EXIST "%~dp0packages\NUnit.ConsoleRunner.3.6.1\" (ECHO Error: Cannot find NUnit Console Runner. Build the project to restore the packages && PAUSE && EXIT /B 1)
%~dp0packages\NUnit.ConsoleRunner.3.6.1\tools\nunit3-console.exe %~dp0test\KitchenSink.Tests\bin\Debug\KitchenSink.Tests.dll --noheader --params Browsers=Chrome

if %interactive%==0 pause

IF ERRORLEVEL 1 EXIT /B 1