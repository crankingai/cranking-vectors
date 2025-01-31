public static class VectorMath
{
   public static ReadOnlyMemory<float> Add(ReadOnlyMemory<float> a, ReadOnlyMemory<float> b)
   {
      if (a.Length != b.Length)
         throw new ArgumentException("Vectors must be of same length");

      var result = new float[a.Length];
      for (int i = 0; i < a.Length; i++)
      {
         result[i] = a.Span[i] + b.Span[i];
      }
      return result;
   }

   public static ReadOnlyMemory<float> Subtract(ReadOnlyMemory<float> a, ReadOnlyMemory<float> b)
   {
      if (a.Length != b.Length)
         throw new ArgumentException("Vectors must be of same length");

      var result = new float[a.Length];
      for (int i = 0; i < a.Length; i++)
      {
         result[i] = a.Span[i] - b.Span[i];
      }
      return result;
   }


   /// <summary>
   /// Calculates the cosine similarity between two embedding vectors.
   /// </summary>
   /// <param name="a">First embedding vector</param>
   /// <param name="b">Second embedding vector</param>
   /// <returns>
   /// A value typically between 0 and 1 for embedding vectors, where:
   /// - Values close to 1 indicate high semantic similarity
   /// - Values close to 0 indicate low semantic similarity
   /// Note: While mathematically possible to get values between -1 and 0,
   /// modern embedding models like text-embedding-ada-002 typically don't
   /// produce negative similarities.
   /// </returns>
   public static float CosineSimilarity(ReadOnlyMemory<float> a, ReadOnlyMemory<float> b)
   {
      if (a.Length != b.Length)
         throw new ArgumentException("Vectors must be of same length");

      var span1 = a.Span;
      var span2 = b.Span;

      float dotProduct = 0;
      float norm1 = 0;
      float norm2 = 0;

      for (int i = 0; i < span1.Length; i++)
      {
         dotProduct += span1[i] * span2[i];
         norm1 += span1[i] * span1[i];
         norm2 += span2[i] * span2[i];
      }

      return dotProduct / (float)(Math.Sqrt(norm1) * Math.Sqrt(norm2));
   }
}
