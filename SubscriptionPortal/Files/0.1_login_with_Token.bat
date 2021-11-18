echo off

echo username: %1
echo userpassword: %2
echo serverlink: %3
echo servertoken: %4

:: login cmd
:: oc login --token=sha256~Uvarl9leHpN34VadO8nJEIpDox1-qYkvhEkAzkxxWfM --server=https://api.sandbox.x8i5.p1.openshiftapps.com:6443

oc login --token=%4 --server=%3