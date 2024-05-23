@echo off
REM Change to the directory of the script
pushd "%~dp0"

REM get the full path of the DAWPresence.exe
for /f "tokens=*" %%a in ('dir /s /b DAWPresence.exe') do set DAWPresencePath=%%a

REM create a scheduled task to run DAWPresence.exe with highest privileges in the background
schtasks /create /tn "DAWPresence" /tr "\"%DAWPresencePath%\"" /sc onstart /rl highest /f

echo DAWPresence scheduled task created
pause

REM Revert to the previous directory
popd
