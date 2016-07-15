setlocal
cd %~dp0

echo Make schema file
gschema2 Goedel.gdl /cs Goedel.cs /lazy

exit /b 0