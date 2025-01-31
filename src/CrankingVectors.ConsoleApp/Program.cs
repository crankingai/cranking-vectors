// Using an OpenAI embedding model hosted on Azure OpenAI, use Semantic Kernel in C# on .NET 9 to programatically (a) use API to create an embedding vector for the string "King of England" then the string "Queen of England" then calculate cosine similarity.

using System.Numerics;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Embeddings;      

var builder = Kernel.CreateBuilder();

#region Configure Azure OpenAI Embedding API
string? deploymentName = Environment.GetEnvironmentVariable("EMB_AZURE_OPENAI_DEPLOYMENT_NAME");
// string? modelId = Environment.GetEnvironmentVariable("EMB_AZURE_OPENAI_MODEL_ID");
string? apiKey = Environment.GetEnvironmentVariable("EMB_AZURE_OPENAI_API_KEY");
string? endpoint = Environment.GetEnvironmentVariable("EMB_AZURE_OPENAI_ENDPOINT");

if (string.IsNullOrEmpty(deploymentName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(endpoint))
{
        throw new InvalidOperationException("OpenAI deployment name, model ID, API key, and endpoint expected to be set in environment variables.");
}

var modelId = "text-embedding-ada-002"; // ada @ 1536 < 3-large @ 256
modelId = "text-embedding-3-large"; // https://devblogs.microsoft.com/azure-sql/embedding-models-and-dimensions-optimizing-the-performance-resource-usage-ratio/

#pragma warning disable SKEXP0010
builder.AddAzureOpenAITextEmbeddingGeneration(
        deploymentName: deploymentName,
        endpoint: endpoint,
        apiKey: apiKey,
        modelId: modelId // embedding model in our case, but otherwise can be any model including inference models
);
#pragma warning restore SKEXP0010
#endregion

var kernel = builder.Build();

// Get the text embedding service
#pragma warning disable SKEXP0001
var embeddingService = kernel.GetRequiredService<ITextEmbeddingGenerationService>();
#pragma warning restore SKEXP0010

var semcomp = new SemanticComparer(embeddingService);

Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("dog", "cat")}");
Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("dog", "puppy")}");
Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("puppy", "kitten")}");
Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("puppy", "calf")}");
Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("kitten", "calf")}");
Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("kitten", "calf")}");
Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("shin", "calf")}");

Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("baby dog", "puppy")}");
Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("kitten", "donut")}");

Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("arithmetic", "coal miner")}");
Console.WriteLine($"{await semcomp.CompareCosineSimilarityAsync("fast", "slow")}");

Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("happy", "ecstatic"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("happy", "miserable"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("democracy", "autocracy"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("quantum physics", "classical mechanics"));

Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("dog", "perro")); // dog in Spanish
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("chat", "cat"));  // cat in French

Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Pablo Picasso", "pablo picasso"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Pablo Picasso", "Picasso"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Pablo Picasso", "picasso"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Pablo Picasso", "Dave picasso"));

var modelString = $"┃ Embedding model used: {modelId} ┃";
var modelStringLine = $"━".PadLeft(modelString.Length - 2, '━');
Console.Error.WriteLine($"┣{modelStringLine}┫");
Console.Error.WriteLine(modelString);
Console.Error.WriteLine($"┣{modelStringLine}┫");

Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("king", "queen"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("queen", "king"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Queen", "King"));

Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("afaf.87sdf6asdf", "table cloth"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("banana", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("crayon", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("toilet paper", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Gothic architecture", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("pinball machine", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("button", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Dr. Seuss", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("Dr. Seuss", "nuclear physicist"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("crayon bag", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("sheet of toilet paper", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("crayons", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("toilet paper sheet", "nuclear physics"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("toilet paper sheet", "nuclear physics textbook"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("toilet paper artwork", "nuclear physics equations"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("crayon box", "box of crayons"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("crayon boxes", "boxes of crayons"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hike", "jogging"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hike", "jog"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking", "jog"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking", "jogging"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking - woods", "jogging"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking", "jogging + woods"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking - woods", "jogging"));

Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking", "walking"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking - woods", "walking"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking + on the street", "walking"));
Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("hiking - woods", "walking on the street"));

Console.WriteLine(await semcomp.CompareCosineSimilarityAsync("queen", "king - man + woman"));
