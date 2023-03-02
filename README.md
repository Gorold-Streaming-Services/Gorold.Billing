## Add the GitHub package source
```powershell
$owner="Gorold-Streaming-Services"
$gh_pat="[PAT HERE]"

dotnet nuget add source --username USERNAME --password $gh_pat --store-password-in-clear-text --name github "https://nuget.pkg.github.com/$owner/index.json"
```
## Create and publish package
```powershell
$version="1.0.4"
$owner="Gorold-Streaming-Services"
$gh_pat="[PAT HERE]"

dotnet pack Gorold.Billing.Contracts/ --configuration Release -p:PackageVersion=$version -p:RepositoryUrl=https://github.com/$owner/gorold.billing -o ../nugets/

dotnet nuget push ../nugets/Gorold.Billing.Contracts.$version.nupkg --api-key $gh_pat --source "github"
```

## Use Azure Container Registry images in local K8s
The steps described here are expressed in this guide my Microsoft: https://learn.microsoft.com/en-us/azure/container-registry/container-registry-auth-kubernetes.

## Creating the Azure resource group
```powershell
$appname="playeconomy"
az group create --name $appname --location eastus
```

## Creating the Cosmos DB account
```powershell
az cosmosdb create --name $appname --resource-group $appname --kind MongoDB --enable-free-tier
```

## Creating the Service Bus namespace
```powershell
az servicebus namespace create --name $appname --resource-group $appname --sku Standard
```

## Creating the Container Registry
```powershell
az acr create --name $appname --resource-group $appname --sku Basic
```

## Publish the Docker image
```powershell
az acr login --name $appname
docker push "$appname.azurecr.io/play.inventory:$version"
```

## Creating the Azure Key Vault
```powershell
az keyvault create -n $appname -g $appname
```

## Key Vault
To have the ability of access KeyVault from within K8s, please refer to: https://learn.microsoft.com/en-us/azure/aks/workload-identity-deploy-cluster#code-try-2