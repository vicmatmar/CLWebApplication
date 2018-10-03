dotnet publish -c Release

::ssh -t user@clweb "sudo systemctl stop clweb.service"
::ssh user@clweb "rm -rf ~/publish/*"

::plink user@clweb pwd
::plink user@clweb ls -la ~/public/publish/

plink -v -ssh -t user@clweb sudo systemctl stop clweb.service
plink user@clweb rm -rf ~/public/publish/*

xcopy /s/i/e bin\release\netcoreapp2.1\publish \\clweb\Public\publish

::ssh -t user@clweb "sudo systemctl start clweb.service"
plink -v -ssh -t user@clweb "sudo systemctl start clweb.service"


goto end
:end