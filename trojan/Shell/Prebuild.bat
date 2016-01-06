setlocal

cd %~dp0

echo Make command file
CommandParse /generate Shell.command /cs Shell.cs /main /nocatch
exit /b 0

