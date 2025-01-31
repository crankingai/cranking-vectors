public class EmbeddingVector
{
    public ReadOnlyMemory<float> Vector { get; }

    public EmbeddingVector(ReadOnlyMemory<float> vector)
    {
        Vector = vector;
    }

    public static EmbeddingVector operator +(EmbeddingVector a, EmbeddingVector b)
        => new(VectorMath.Add(a.Vector, b.Vector));

    public static EmbeddingVector operator -(EmbeddingVector a, EmbeddingVector b)
        => new(VectorMath.Subtract(a.Vector, b.Vector));

    public static implicit operator ReadOnlyMemory<float>(EmbeddingVector v) => v.Vector;
    public static implicit operator EmbeddingVector(ReadOnlyMemory<float> v) => new(v);
}