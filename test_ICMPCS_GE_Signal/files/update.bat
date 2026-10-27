@echo off
set currd=%date:~10,4%-%date:~4,2%-%date:~7,2%
set currms=%time:~2,6%
set /a currh=%time:~0,2%
if %currh% LSS 10 set currh=0%currh%
set currt=%currh%%currms%
set PriConfig=<<<IREPOP>>>\<<<SFOLDER>>>


REM ************** edit here only if needed **************

set record=<<<DCSR>>>,<<<DMST>>>,<<<DMWF>>>




REM *************** do not edit below here ***************

call :retry
goto:eof

:retry
set /a tries=300

:loop
if %tries% LEQ 0 goto return
echo <<<STARTTS>>>,%currd% %currt%,<<<UTC>>>,VN,<<<%username%>>>,<<<UNDERDEV>>>,%record%>>%PriConfig%\VF_CE.txt && goto return || set /a tries-=1 && ping -n 1 127.0.0.1>nul 2>&1 && goto loop

:return
@exit /B