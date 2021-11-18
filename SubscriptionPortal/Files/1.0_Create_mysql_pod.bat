@echo off
echo podname: %1
echo dbname: %2
echo dbusername: %3
echo dbpassword: %4
echo rootpassword: %5


:: create mysql pod  --as deployment_config
:: oc new-app --name=mysqldb   MYSQL_USER=dbusername MYSQL_PASSWORD=dbpassword MYSQL_ROOT_PASSWORD=dbpassword MYSQL_DATABASE=mysqldb registry.access.redhat.com/openshift3/mysql-55-rhel7

oc new-app --name=%1   MYSQL_DATABASE=%2    MYSQL_USER=%3  MYSQL_PASSWORD=%4  MYSQL_ROOT_PASSWORD=%5   registry.access.redhat.com/openshift3/mysql-55-rhel7

