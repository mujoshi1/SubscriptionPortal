param(
  [CmdletBinding()]
  # Our preferred encoding
  # [parameter(Mandatory=$false)]
  # [ValidateSet("UTF8","Unicode","UTF7","ASCII","UTF32","BigEndianUnicode")]
  #[Parameter(Mandatory = $true,ValueFromPipeline = $true,ValueFromPipelineByPropertyName = $true)]
  #[ValidateNotNullOrEmpty()]
  #[Alias('FullName')]
    [string] $username,
    [string] $userpassword,
    [string] $serverlink,
    [string] $servertoken,    
    [string] $containername,
    [string] $podname,
    [string] $dbname,
    [string] $dbusername,
    [string] $dbpassword,
    [string] $rootuser,
    [string] $rootpassword,
    # .Net appliciaon variables
    [string] $envname,
    [string] $appname
)

#cls
#Set-ExecutionPolicy -Scope Process -ExecutionPolicy Unrestricted; Get-ExecutionPolicy
#Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope LocalMachine
#Set-ExecutionPolicy -Scope CurrentUser -ExecutionPolicy RemoteSigned;


write ("Encoding : {0}" -f $Encoding)

Write-Output $username
Write-Output $userpassword
Write-Output $serverlink
Write-Output $servertoken
Write-Output $containername
Write-Output $podname
Write-Output $dbname
Write-Output $dbusername
Write-Output $dbpassword
Write-Output $rootuser
Write-Output $rootpassword

Write-Output $envname
Write-Output $appname


# parameters 
if($username -eq $Null)
{
    $username='mujoshi1'
}
if($userpassword  -eq $Null)
{
    $userpassword='Welcome@IBM9535084436'
}
if($serverlink  -eq $Null)
{
    $serverlink= 'https://api.sandbox.x8i5.p1.openshiftapps.com:6443'
}
if($servertoken  -eq $Null)
{
    $servertoken= 'sha256~dCDKlZ5DVXxVub8nYlQGTn3pX5cLFHnDOWNXph7mmIo' 
}
if($containername  -eq $Null)
{
    $containername= 'mysqldbdefault'
}
if($podname  -eq $Null)
{
    $podname= 'mysqldbdefault'
}
if($dbname  -eq $Null)
{
    $dbname= 'mysqldbdefault'
}
if($dbusername  -eq $Null)
{
    $dbusername= 'dbusername'
}
if($dbpassword  -eq $Null)
{
    $dbpassword= 'dbpassword'
}
if($rootuser  -eq $Null)
{
    $rootuser= "root"
}
if($rootpassword  -eq $Null)
{
    $rootpassword= 'dbpassword'
}

$mpod= ''

# dot net appliciaon variables
if($envname  -eq $Null)
{
    $envname= 'Development'
}
if($appname  -eq $Null)
{
    $appname= 'hrappyr'
}

timeout /t 2

Start-Transaction

#$transactedString = New-Object Microsoft.PowerShell.Commands.Management.TransactedString

$servertoken=  (oc whoami -t)
Write-Output ${servertoken}

#$transactedString.Append( 
oc login --token=$servertoken --server=https://api.sandbox.x8i5.p1.openshiftapps.com:6443
#)
#Use-Transaction -TransactedScript{
#$transactedString.Append (
oc project "mujoshi1-2-stage"

Write-Output "login successfully"
Write-Output $servertoken

timeout /t 2

Write-Output "create mysql pod  as deployment_config"
oc new-app --name=$podname  MYSQL_DATABASE=$dbname  MYSQL_USER=$dbusername  MYSQL_PASSWORD=$dbpassword  MYSQL_ROOT_PASSWORD=$rootpassword  registry.access.redhat.com/openshift3/mysql-55-rhel7
Write-Output "create mysql pod"

timeout /t 2
#oc new-app --name=hrappyrr  MYSQL_DATABASE=hrappyrr  MYSQL_USER=hrappyrr  MYSQL_PASSWORD=dbpassword  MYSQL_ROOT_PASSWORD=rootpassword  registry.access.redhat.com/openshift3/mysql-55-rhel7

#) -UseTransaction
Stop-Transcript
Complete-Transaction
