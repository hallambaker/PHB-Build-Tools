setlocal
cd %~dp0
exit /b 0

echo Make schema file
rfctool /rfc ..\..\Libraries\Goedel.Cryptography\CryptographyLibraries.md /aml Content\CryptographyLibraries.aml
rfctool /rfc ..\..\Libraries\Goedel.Protocol\ProtocolLibraries.md /aml Content\ProtocolLibraries.aml
rfctool /rfc ..\..\Libraries\Goedel.Utilities\TextConversion.md /aml Content\TextConversion.aml
rfctool /rfc ..\..\Libraries\Goedel.IO\FileTools.md /aml Content\FileTools.aml

rfctool /rfc ..\Markdown\CodeStandards.md /aml Content\CryptographyLibraries.aml
rfctool /rfc ..\Markdown\Libraries.md /aml Content\CryptographyLibraries.aml
rfctool /rfc ..\Markdown\Welcome.md /aml Content\Welcome.aml

rfctool /rfc ..\..\VSIX\VSIXIntegration.md /aml Content\CryptographyLibraries.aml
rfctool /rfc ..\..\Exceptional\Exceptional.md /aml Content\Exceptional.aml
rfctool /rfc ..\..\RFCTool\DocumentTools.md /aml Content\DocumentTools.aml

exit /b 0