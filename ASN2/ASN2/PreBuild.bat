setlocal

cd %~dp0

echo Make schema file
gschema3  Asn2.gdl /cs Asn2.cs /lazy

echo Make script file
gscript GenerateCS.script GenerateCS.cs /lazy

exit /b 0