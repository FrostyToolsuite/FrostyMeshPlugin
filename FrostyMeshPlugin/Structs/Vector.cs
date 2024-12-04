// ReSharper disable once CheckNamespace
namespace System.Numerics;

public struct Vector4<T> : IEquatable<Vector4<T>>, IFormattable where T : unmanaged
{
    public T X, Y, Z, W;

    public Vector4(T inX, T inY, T inZ, T inW)
    {
        X = inX;
        Y = inY;
        Z = inZ;
        W = inW;
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        FormattableString formattable = $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Z)}: {Z}, {nameof(W)}: {W}";
        return formattable.ToString(formatProvider);
    }

    public bool Equals(Vector4<T> other)
    {
        return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);
    }

    public override bool Equals(object? obj)
    {
        return obj is Vector4<T> other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z, W);
    }
}