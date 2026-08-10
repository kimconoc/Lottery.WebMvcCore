@echo off
setlocal
set "PROJ=%~dp0Lottery.WebMvc\Lottery.WebMvc.csproj"
set "OUT=D:\Lottery.WebMvcCore.Public"

echo Stopping Lottery.WebMvc if running...
taskkill /F /IM Lottery.WebMvc.exe >nul 2>&1

echo Cleaning...
dotnet clean "%PROJ%" -c Release
if errorlevel 1 exit /b 1

echo Publishing to %OUT% ...
dotnet publish "%PROJ%" -c Release -o "%OUT%" --force /p:DeleteExistingFiles=true /p:PublishProfile=FolderProfile
if errorlevel 1 exit /b 1

echo.
echo Done. Published files:
dir /T:W "%OUT%\Lottery.*.dll" "%OUT%\Lottery.WebMvc.exe"
endlocal
