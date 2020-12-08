SETLOCAL
@echo "Current Directory %cd%"
@echo "Batch file is in %~dp0"

cd %~dp0

gscript /wrap TagDefinitions.mdsd /cs /namespace BridgeLib /lazy
gscript /wrap Template.md /cs /namespace BridgeLib /variable Template /lazy
exit /b 0


