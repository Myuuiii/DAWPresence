@echo off

REM delete the scheduled task named "DAWPresence"
schtasks /delete /tn "DAWPresence" /f

echo DAWPresence scheduled task deleted
pause
