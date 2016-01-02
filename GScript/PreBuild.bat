SETLOCAL
@echo "Current Directory %cd%"
@echo "Batch file is in %~dp0"

cd %~dp0

CommandParse Goedel.command /cs Goedel.cs /lazy /nocatch /builtins
exit /b 0


