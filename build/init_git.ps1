git config --global core.autocrlf true
git config --global user.email "walia.rubal@gmail.com"
git config --global user.name "Rubal Walia"
git config --global credential.helper store
Add-Content "$HOME\.git-credentials" "https://$($env:access_token):x-oauth-basic@github.com`n"