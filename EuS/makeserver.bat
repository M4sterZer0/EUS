mkdir server
mkdir server\ServerFiles
mkdir server\ClientFiles
xcopy /E /S /Y ServerFiles server\ServerFiles /exclude:excludedfiles.txt
xcopy /E /S /Y ClientFiles server\ClientFiles /exclude:excludedfiles.txt
copy meta.xml server
copy bin\Debug\EuS.dll server