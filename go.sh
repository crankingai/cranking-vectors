#!/bin/bash

#az cognitiveservices account deployment create --name bill-m3aiggjm-canadaeast --resource-group crankingai-rg --deployment-name bill-m3aiggjm-canadaeast --model-name text-embedding-3-large --model-version 1 --model-format OpenAI

# az cognitiveservices account list --output table

#Kind        Location       Name                      ResourceGroup
#----------  -------------  ------------------------  ---------------
#AIServices  eastus         bill-m57k447n-eastus      crankingai-rg
#OpenAI      canadacentral  skdev-openai              skdev-rg
#AIServices  canadacentral  crankingai                crankingai-rg
#OpenAI      canadaeast     bill-m3aiggjm-canadaeast  skdev-rg

AOAI_Name=bill-m3aiggjm-canadaeast
AOAI_RG=skdev-rg

AOAI_Name=bill-m57k447n-eastus
AOAI_RG=crankingai-rg

RG=lab-rg
SUB_NAME=EffAz-Dev
SUB_ID=$(az account show --subscription $SUB_NAME --query id -o tsv)
az account set --subscription EffAz-Dev

#################
#### START OVER BY SCRUBBING THE RESOURCE GROUP ####
echo "About to delete resource group $RG"
az group delete --name $RG --yes
#################

#### CONTINUE...
echo "About to re-create resource group $RG"
az group create --name $RG --location eastus

echo "Let's go.."

RES_NAME=workshop-labs
DEPLOY_NAME=vectorembed
LOC=canadaeast
LOC=eastus2
LOC=eastus
SKU=Global
SKU=Standard
SKU=GlobalProvisionedManaged
SKU=GlobalStandard
SKU=S1
SKU=S0 # only type that supports OpenAI, per `az cognitiveservices account list-skus --location eastus --output table | grep -i open`
SKU_NAME=GlobalProvisionedManaged
SKU_NAME=GlobalStandard
SKU_NAME=S1
SKU_NAME=S0 # only type that supports OpenAI, per `az cognitiveservices account list-skus --location eastus --output table | grep -i open`
SKU_NAME=GlobalStandard
SKU_NAME=Global
SKU_NAME=Standard

echo "Purging OpenAI resource $RES_NAME in $RG"
az cognitiveservices account purge \
  --name $RES_NAME \
  --resource-group $RG \
  --location $LOC \
  --subscription $SUB_ID

echo "Creating OpenAI resource $RES_NAME in $RG"
az cognitiveservices account create \
--name $RES_NAME \
--resource-group $RG \
--kind OpenAI \
--location $LOC \
--sku $SKU \
--kind OpenAI \
--subscription $SUB_ID

echo "Show SKUs available"
az cognitiveservices account show --name $RES_NAME --resource-group $RG --query sku

echo "Creating deployment $DEPLOY_NAME"
az cognitiveservices account deployment create \
--name $RES_NAME --resource-group $RG \
--deployment-name $DEPLOY_NAME \
--model-name text-embedding-3-large \
--sku-name $SKU_NAME \
--sku-capacity 15 \
--model-version 1 --model-format OpenAI

echo "Show account"
az cognitiveservices account show \
  --name $RES_NAME \
  --resource-group $RG

echo "Show keys"
az cognitiveservices account keys list \
  --name $RES_NAME \
  --resource-group $RG
