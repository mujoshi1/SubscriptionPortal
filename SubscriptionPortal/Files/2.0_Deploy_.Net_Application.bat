@echo off
echo envname: %1
echo appname: %2
echo podname: %3
echo dbname: %4
echo dbusername: %5
echo dbpassword: %6
echo rootpassword: %7


:: deploy  OpenShiftHrApp  .Net application with Dont net core framework
:: oc new-app --name=openshifthrapp dotnet:3.1~https://github.com/mujoshi1/OpenShiftHrOffice.git#master --context-dir HrOffice

oc new-app --name=%2 dotnet:3.1~https://github.com/mujoshi1/OpenShiftHrOffice#master --context-dir HrOffice


timeout /t 4

:: Set  .Net application Environment  adn sqldb connection

:: oc set env --overwrite pod/${mpod} ASPNETCORE_ENVIRONMENT=Development  DBHOST=mysqldbyr  DBPORT=3306  DBNAME=mysqldbyr  DBUSER=dbusername  DBPASSWORD=dbpassword 

oc set env  --overwrite pod/%3  ASPNETCORE_ENVIRONMENT=%1  DBHOST=%4  DBPORT=3306  DBNAME=%4  DBUSER=%5  DBPASSWORD=%6 

timeout /t 4
:: OR

:: oc set env --overwrite  deployment/openshifthrapp ASPNETCORE_ENVIRONMENT=Development  DBHOST=mysqldbyr  DBPORT=3306  DBNAME=mysqldbyr  DBUSER=dbusername  DBPASSWORD=dbpassword 

oc set env  --overwrite  deployment/%2  ASPNETCORE_ENVIRONMENT=%2  DBHOST=%4  DBPORT=3306  DBNAME=%4  DBUSER=%5  DBPASSWORD=%6

timeout /t 4

:: oc expose service openshifthrapp
oc expose service %2

timeout /t 4

:: oc get route openshifthrapp
oc get route %2