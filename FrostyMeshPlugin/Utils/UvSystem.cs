using System;
using System.Diagnostics.CodeAnalysis;

namespace FrostyMeshPlugin.Utils;

public struct UvSystem : IEquatable<UvSystem>
{
    public CoordinateAxis Up;
    public CoordinateAxis Right;

    public static bool operator ==(UvSystem a, UvSystem b) => a.Equals(b);

    public static bool operator !=(UvSystem a, UvSystem b) => !a.Equals(b);
    
    public bool Equals(UvSystem other) => Up == other.Up && Right == other.Right;
    
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is UvSystem other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Up, Right);
    }
}