SETLOCAL
@echo "Current Directory %cd%"
@echo "Batch file is in %~dp0"

cd %~dp0

CommandParse Main.command /cs Main.cs /lazy /nocatch
exit /b 0


