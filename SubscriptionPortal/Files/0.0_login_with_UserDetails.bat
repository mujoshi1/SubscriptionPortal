@echo off

echo username: %1
echo userpassword: %2
echo serverlink: %3

:: login cmd
:: oc login  -u=mujoshi1  -p=Welcome@IBM9535084436 --server=https://api.sandbox.x8i5.p1.openshiftapps.com:6443
oc login  -u=%1  -p=%2 --server=%3