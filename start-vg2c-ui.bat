@echo off
setlocal
cd /d "%~dp0"
set "PATH=%~dp0.venv\Scripts;%PATH%"
echo Building frontend...
pushd "%~dp0src\vg2c_ui\frontend"
call npm.cmd ls --depth=0 >nul 2>&1
if errorlevel 1 (
	echo Installing frontend dependencies...
	call npm.cmd ci
	if errorlevel 1 (
		echo Frontend dependency installation failed. Server not started.
		popd
		exit /b 1
	)
)
call npm.cmd run build
if errorlevel 1 (
	echo Frontend build failed. Server not started.
	popd
	exit /b 1
)
popd
echo Open http://127.0.0.1:8765/
"%~dp0.venv\Scripts\python.exe" -m vg2c_ui --data-dir "%~dp0data"
