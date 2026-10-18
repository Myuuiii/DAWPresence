@echo off
setlocal EnableDelayedExpansion

rem Build DAWPresence app + installer and collect artifacts into tools\output\.

set "SCRIPT_DIR=%~dp0"
set "REPO_ROOT=%SCRIPT_DIR%.."
set "OUTPUT_DIR=%SCRIPT_DIR%output"
set "CONFIGURATION=Release"
set "SOLUTION=%REPO_ROOT%\DAWPresence.sln"
set "APP_PROJECT=%REPO_ROOT%\App\App.csproj"
set "INSTALLER_PROJECT=%REPO_ROOT%\Installer\Installer.wixproj"
set "VERSION_FILE=%REPO_ROOT%\App\version.txt"

if not exist "%VERSION_FILE%" (
    echo [build] version.txt not found at %VERSION_FILE%
    exit /b 1
)
set /p APP_VERSION=<"%VERSION_FILE%"
set "APP_VERSION=%APP_VERSION: =%"
echo [build] Version: %APP_VERSION%

where dotnet >nul 2>&1
if errorlevel 1 (
    echo [build] dotnet SDK not found on PATH.
    exit /b 1
)

if not exist "%OUTPUT_DIR%" mkdir "%OUTPUT_DIR%"

rem Installer.wixproj uses $(SolutionDir) to resolve App\VERSION.txt, so dotnet
rem must be invoked with the repo root as the working directory.
pushd "%REPO_ROOT%" || exit /b 1

echo [build] Restoring solution...
dotnet restore "%SOLUTION%"
if errorlevel 1 ( popd & exit /b 1 )

echo [build] Publishing App (%CONFIGURATION%, single-file)...
set "APP_PUBLISH_DIR=%REPO_ROOT%\App\publish"
if exist "%APP_PUBLISH_DIR%" rmdir /s /q "%APP_PUBLISH_DIR%"
dotnet publish "%APP_PROJECT%" -c %CONFIGURATION% -r win-x64 -p:PublishSingleFile=true -p:SelfContained=false -p:Platform=x64 -o "%APP_PUBLISH_DIR%"
if errorlevel 1 ( popd & exit /b 1 )

echo [build] Building installer...
dotnet build "%INSTALLER_PROJECT%" -c %CONFIGURATION% -p:Platform=x64
if errorlevel 1 ( popd & exit /b 1 )

popd

set "INSTALLER_MSI="
for %%F in (
    "%REPO_ROOT%\Installer\bin\x64\%CONFIGURATION%\en-US\DAWPresence-%APP_VERSION%-installer.msi"
    "%REPO_ROOT%\Installer\bin\%CONFIGURATION%\en-US\DAWPresence-%APP_VERSION%-installer.msi"
    "%REPO_ROOT%\Installer\bin\x64\%CONFIGURATION%\DAWPresence-%APP_VERSION%-installer.msi"
    "%REPO_ROOT%\Installer\bin\%CONFIGURATION%\DAWPresence-%APP_VERSION%-installer.msi"
) do (
    if exist %%F if not defined INSTALLER_MSI set "INSTALLER_MSI=%%~F"
)
if not defined INSTALLER_MSI (
    echo [build] Installer MSI not found under %REPO_ROOT%\Installer\bin
    exit /b 1
)

set "PORTABLE_STAGE=%OUTPUT_DIR%\DAWPresence-v%APP_VERSION%-portable"
set "PORTABLE_ZIP=%OUTPUT_DIR%\DAWPresence-v%APP_VERSION%-portable.zip"
if exist "%PORTABLE_STAGE%" rmdir /s /q "%PORTABLE_STAGE%"
if exist "%PORTABLE_ZIP%" del /q "%PORTABLE_ZIP%"
mkdir "%PORTABLE_STAGE%"

echo [build] Staging portable app to %PORTABLE_STAGE%
xcopy /e /i /y /q "%APP_PUBLISH_DIR%\*" "%PORTABLE_STAGE%\" >nul || exit /b 1

echo [build] Zipping portable app to %PORTABLE_ZIP%
powershell -NoProfile -Command "Compress-Archive -Path '%PORTABLE_STAGE%\*' -DestinationPath '%PORTABLE_ZIP%' -Force" || exit /b 1
rmdir /s /q "%PORTABLE_STAGE%"

echo [build] Copying installer to %OUTPUT_DIR%
copy /y "%INSTALLER_MSI%" "%OUTPUT_DIR%\DAWPresence-%APP_VERSION%-installer.msi" >nul || exit /b 1

echo.
echo [build] Done.
echo   Portable:  %PORTABLE_ZIP%
echo   Installer: %OUTPUT_DIR%\DAWPresence-%APP_VERSION%-installer.msi
endlocal
