@echo off
setlocal
cd /d "%~dp0"
if exist ".venv\Scripts\pythonw.exe" (
  ".venv\Scripts\pythonw.exe" -m vg2c_companion %*
  goto done
)
if exist ".venv\Scripts\python.exe" (
  ".venv\Scripts\python.exe" -m vg2c_companion %*
  goto done
)
where vg2c-companion >nul 2>nul
if %errorlevel%==0 (
  vg2c-companion %*
  goto done
)
echo SQLPathFinder companion requires the repository .venv or an installed vg2c-companion command.
exit /b 1
:done
set status=%errorlevel%
endlocal & exit /b %status%
