@ECHO OFF

IF "%CONFIGURATION%"=="" SET CONFIGURATION=Debug

star --resourcedir="%~dp0src\KitchenSink\wwwroot" "%~dp0bin/%CONFIGURATION%/KitchenSink.exe"