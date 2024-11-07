// ReSharper disable once CheckNamespace
namespace System.Numerics;

public struct BoundingBox : IEquatable<BoundingBox>, IFormattable
{
    public Vector4 Min;
    public Vector4 Max;

    public BoundingBox()
    {
    }

    public BoundingBox(Vector4 inMin, Vector4 inMax)
    {
        Min = inMin;
        Max = inMax;
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        FormattableString formattable = $"{nameof(Min)}: {Min}, {nameof(Max)}: {Max}";
        return formattable.ToString(formatProvider);
    }

    public override string ToString()
    {
        return $"{nameof(Min)}: {Min}, {nameof(Max)}: {Max}";
    }

    public bool Equals(BoundingBox other)
    {
        return Min.Equals(other.Min) && Max.Equals(other.Max);
    }

    public override bool Equals(object? obj)
    {
        return obj is BoundingBox other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Min, Max);
    }
}