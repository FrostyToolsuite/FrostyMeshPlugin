using System;
using System.Diagnostics.CodeAnalysis;

namespace FrostyMeshPlugin.Utils;

public struct CoordinateSystem : IEquatable<CoordinateSystem>
{
    /// <summary>
    /// The axis pointing up.
    /// </summary>
    public CoordinateAxis Up;
    
    /// <summary>
    /// The axis pointing forward (into the screen).
    /// </summary>
    public CoordinateAxis Forward;
    
    /// <summary>
    /// The axis pointing right.
    /// </summary>
    public CoordinateAxis Right;

    public static bool operator ==(CoordinateSystem a, CoordinateSystem b) => a.Equals(b);

    public static bool operator !=(CoordinateSystem a, CoordinateSystem b) => !a.Equals(b);

    public bool Equals(CoordinateSystem other) => Up == other.Up && Forward == other.Forward && Right == other.Right;
    
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is CoordinateSystem other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Up, Forward, Right);
    }
}