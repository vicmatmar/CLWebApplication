ssh -t user@clweb "sudo systemctl stop clweb.service"
ssh user@clweb "rm -rf ~/publish/*"

dotnet publish -c Release
xcopy /s/i/e bin\release\netcoreapp2.1\publish \\clweb\Public\publish

ssh -t user@clweb "sudo systemctl start clweb.service"

