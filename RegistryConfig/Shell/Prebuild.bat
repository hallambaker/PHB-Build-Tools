setlocal

cd %~dp0

echo Make command file
CommandParse Shell.command /cs Shell.cs /main
exit /b 0

