SETLOCAL
@echo "Current Directory %cd%"
@echo "Batch file is in %~dp0"

cd %~dp0

rem CommandParse NewCommands.cmd /cs Commands.cs /nocatch
exit /b 0


