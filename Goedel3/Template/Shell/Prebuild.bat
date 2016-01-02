setlocal

cd %~dp0

echo Make command file
CommandParse Shell.command Shell.cs /main
exit /b 0

