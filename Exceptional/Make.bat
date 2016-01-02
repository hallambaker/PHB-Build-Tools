setlocal

cd %~dp0



echo Make command file
CommandParse Main.Command Main.cs /lazy

exit /b 0