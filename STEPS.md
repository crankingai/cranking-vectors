# Steps to build and configure

## Which SKUs align with which REGIONS

https://learn.microsoft.com/en-us/azure/ai-services/openai/concepts/models?tabs=global-standard%2Cstandard-chat-completions#model-summary-table-and-region-availability

```bash
echo "SKU list in location"
az cognitiveservices account list-skus --location eastus --output table
echo "Model list for account"
az cognitiveservices account model list --name $RES_NAME --resource-group $RG --output table
```

## 

Requests to the Embeddings_Create Operation under Azure OpenAI API version 2024-10-01-preview have exceeded call rate limit of your current OpenAI S0 pricing tier. Please retry after 10 seconds. Please go here: https://aka.ms/oai/quotaincrease if you would like to further increase the default rate limit.
 ---> System.ClientModel.ClientResultException: HTTP 429 (429)




(UnsupportedLocationForSku) The location 'GLOBAL' is not supported for SKU 'S0'.
Code: UnsupportedLocationForSku
Message: The location 'GLOBAL' is not supported for SKU 'S0'.
(ResourceNotFound) The Resource 'Microsoft.CognitiveServices/accounts/workshop-labs' under resource group 'lab-rg' was not found. For more details please go to https://aka.ms/ARMResourceNotFoundFix
Code: ResourceNotFound


```bash
az login

az cognitiveservices account list --output table

az cognitiveservices account deployment create \
  --name crankingai \
  --resource-group crankingazure-rg \
  --deployment-name myDeployment \
  --model-name text-embedding-3-large \
  --model-version 1

```

```plaintext
Kind        Location       Name                      ResourceGroup
----------  -------------  ------------------------  ---------------
AIServices  eastus         bill-m57k447n-eastus      crankingai-rg
OpenAI      canadacentral  skdev-openai              skdev-rg
AIServices  canadacentral  crankingai                crankingai-rg
OpenAI      canadaeast     bill-m3aiggjm-canadaeast  skdev-rg
```

AOAI_Name=bill-m3aiggjm-canadaeast
AOAI_RG=crankingai-rg

az cognitiveservices account deployment create --name $AOAI_Name --resource-group $AOAI_RG --deployment-name vectorembed --model-name text-embedding-3-large --model-version 1 --model-format OpenAI

