using System;

namespace FrostyMeshPlugin.Utils;

public struct CoordinateAxis
{
    public Axis Axis;
    public int Sign = 1;

    public CoordinateAxis()
    {
        Axis = Axis.X;
    }

    public override string ToString()
    {
        return $"{(Sign == -1 ? "-" : string.Empty)}{Axis}";
    }

    public static bool operator ==(CoordinateAxis a, CoordinateAxis b) => a.Equals(b);

    public static bool operator !=(CoordinateAxis a, CoordinateAxis b) => !a.Equals(b);
    
    public static implicit operator CoordinateAxis(string value) => Parse(value);
    
    public static implicit operator string(CoordinateAxis value) => value.ToString();

    public static implicit operator int(CoordinateAxis value) => (int)value.Axis;
    
    public bool Equals(CoordinateAxis other)
    {
        return Axis == other.Axis && Sign == other.Sign;
    }

    public override bool Equals(object? obj)
    {
        return obj is CoordinateAxis other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Axis, Sign);
    }

    private static CoordinateAxis Parse(string value)
    {
        CoordinateAxis retVal = default;
        if (value[0] == '-')
        {
            retVal.Sign = -1;
        }
        else
        {
            retVal.Sign = 1;
        }

        retVal.Axis = Enum.Parse<Axis>(value[retVal.Sign == 1 ? .. : 1..]);

        return retVal;
    }
}