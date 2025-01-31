using Microsoft.SemanticKernel.Embeddings;

#pragma warning disable SKEXP0001

public static class EmbeddingExtensions
{
    public static async Task<ReadOnlyMemory<float>> ToEmbeddingAsync(
        this string text,
        ITextEmbeddingGenerationService embeddingService)
    {
        return await embeddingService.GenerateEmbeddingAsync(text);
    }
}

#pragma warning restore SKEXP0001
