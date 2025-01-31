using Microsoft.SemanticKernel.Embeddings;

#pragma warning disable SKEXP0001
public class SemanticComparer
{
    private readonly ITextEmbeddingGenerationService _embeddingService;

    public SemanticComparer(ITextEmbeddingGenerationService embeddingService)
    {
        _embeddingService = embeddingService;
    }

    public string GenerateBarChart(float similarity, bool fancy = true)
    {
        // Round to nearest 0.05 to determine number of bars to allocate
        // 0.05 is matched with similarity:F2 formatting below
        var roundedSimilarity = (float)Math.Round(similarity / 0.05) * 0.05f;

        // Create bar chart
        int filledSlots = (int)(roundedSimilarity * 20);

        string barChart;
        if (fancy)
        {
            barChart = new string('┃', filledSlots) + new string('·', 20 - filledSlots);
        }
        else
        {
            barChart = new string('|', filledSlots) + new string('.', 20 - filledSlots);
        }

        // ┃·【】〔〕⟪⟫ «»
        return $"【{barChart}】⟪{similarity:F2}⟫";
    }

    public async Task<float> CompareCosineSimilarityExpressionsAsync(string expr1, string expr2)
    {
        var embedding1 = await ParseAndEvaluateExpressionAsync(expr1);
        var embedding2 = await ParseAndEvaluateExpressionAsync(expr2);
        var similarity = VectorMath.CosineSimilarity(embedding1, embedding2);
        return similarity;
    }

    public async Task<string> CompareCosineSimilarityAsync(string expr1, string expr2)
    {
        var embedding1 = await ParseAndEvaluateExpressionAsync(expr1);
        var embedding2 = await ParseAndEvaluateExpressionAsync(expr2);
        var similarity = VectorMath.CosineSimilarity(embedding1, embedding2);
        var barChart = GenerateBarChart(similarity);
        return $"{barChart} → '{expr1}' vs '{expr2}'";
    }

    private async Task<ReadOnlyMemory<float>> ParseAndEvaluateExpressionAsync(string expression)
    {
        var terms = expression.Split(new[] { '+', '-' }, StringSplitOptions.TrimEntries);
        var ops = expression.Where(c => c == '+' || c == '-').Select(c => c.ToString()).ToList();

        // Get first term's embedding
        var currentVector = await _embeddingService.GenerateEmbeddingAsync(terms[0]);

        // Process each operation sequentially
        for (int i = 0; i < ops.Count; i++)
        {
            var nextTermEmbedding = await _embeddingService.GenerateEmbeddingAsync(terms[i + 1]);

            currentVector = ops[i] switch
            {
                "+" => VectorMath.Add(currentVector, nextTermEmbedding),
                "-" => VectorMath.Subtract(currentVector, nextTermEmbedding),
                _ => throw new ArgumentException($"Unsupported operator: {ops[i]}")
            };
        }

        return currentVector;
    }
}

#pragma warning restore SKEXP0001
