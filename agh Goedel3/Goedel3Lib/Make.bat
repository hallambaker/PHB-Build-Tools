setlocal
cd %~dp0

echo Make script file
gscript ParserGenerator.script ParserGenerator.cs /lazy

echo Make schema file
gschema2 Goedel.gdl /cs Goedel.cs /lazy

exit /b 0