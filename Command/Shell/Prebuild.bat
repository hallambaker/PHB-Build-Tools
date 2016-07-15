setlocal

cd %~dp0

echo Make command file
CommandParse NewCommandLine.command /cs CommandLine.cs /main
exit /b 0

