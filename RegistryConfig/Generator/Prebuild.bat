setlocal

cd %~dp0

echo Make script file
gscript Generate.script Generate.cs /lazy

echo Make schema file
gschema3 RegistrySchema.gdl /cs RegistrySchema.cs /lazy

exit /b 0
