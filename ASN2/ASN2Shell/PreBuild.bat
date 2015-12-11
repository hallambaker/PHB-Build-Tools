setlocal

cd %~dp0



echo Make command file
CommandParse ASN2.Command ASN2.cs /lazy /nocatch

exit /b 0