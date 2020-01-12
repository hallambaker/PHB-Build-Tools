setlocal
cd %~dp0
set Root=../../..
set DocSource=O:\Documents\Tools

echo Convert documents to TXT, XML and HTML formats

cd ..\Documentation\Publish

copy ..\xml2rfc.css .
copy ..\xml2rfc.js .
copy ..\bib.xml .
copy ..\favicon.png .

rfctool %DocSource%\hallambaker-rfctool.docx  /cache=bib.xml /html all\hallambaker-rfctool.html

exit /b 0

