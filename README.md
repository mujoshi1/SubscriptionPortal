# Subscription Portal
test for Openshift
.NET Core Sample App for OpenShift
This repository contains a simple MVC .NET Core application that can be deployed on OpenShift.

The example is meant to be built and run with the s2i-dotnetcore builder images. The branches of this repository correspond to versions of the s2i-dotnetcore images.

Deploying the application
Deploy using the OpenShift client ('oc')
# Create a new OpenShift project
$ oc new-project mydemo

# Add the .NET Core application
$ oc new-app --name=SubscriptionPortal dotnet:3.1~https://github.com/mujoshi1/SubscriptionPortal#master --context-dir SubscriptionPortal

# Make the .NET Core application accessible externally and show the url
$ oc expose service OpenShiftHrApp
$ oc get route OpenShiftHrApp
Deploy using the OpenShift Do ('odo')
# Use git to check out the .NET Core application
$ git clone https://github.com/mujoshi1/SubscriptionPortal
$ cd SubscriptionPortal/SubscriptionPortal
$ git checkout dotnetcore-3.1

# Create a new OpenShift project
$ odo project create mydemoapp

# Add a component for the .NET Core application
$ odo create dotnet:3.1

# Make the .NET Core application accessible externally
$ odo url create

# Deploy the application
$ odo push
