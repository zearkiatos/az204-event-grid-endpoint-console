# Azure steps to create the resources en Azure

## Step 1: Login

```sh
$ az login --tenant <Your subscription id>
```

## Step 2: Create the resource group

```sh
$ az group create --name myResourceGroup --location eastus
```

## Step 3: Declare variables

```sh
let rNum=$RANDOM
resourceGroup=myResourceGroup
location=eastus
topicName="mytopic-evgtopic-${rNum}"
siteName="evgsite-${rNum}"
siteURL="https://${siteName}.azurewebsites.net"
```

## Step 4: Registry Event grid provider

```sh
$ az provider register --namespace Microsoft.EventGrid

# Checking event grid provider
$ az provider show --namespace Microsoft.EventGrid --query "registrationState"
```

## Step 5: Create the event grid topic

```sh
$ az eventgrid topic create --name $topicName --location $location --resource-group $resourceGroup
```

## Step 6: Create a message endpoint (event grid viewer page)

```sh
$ az deployment group create \
    --resource-group $resourceGroup \
    --template-uri "https://raw.githubusercontent.com/Azure-Samples/azure-event-grid-viewer/main/azuredeploy.json" \
    --parameters siteName=$siteName hostingPlanName=viewerhost

$ echo "Your web app URL: ${siteURL}"
```