setlocal
cd %~dp0

echo Make schema file
rfctool /rfc ..\..\Libraries\Goedel.Cryptography\CryptographyLibraries.md /aml Content\CryptographyLibraries.aml

exit /b 0