using System.Numerics;
using Frosty.Sdk.IO;

namespace FrostyDataStreamUtils;

public static class NumericsExtensions
{
    public static Vector2 ReadVector2(this DataStream stream, bool inPad = false)
    {
        float x = stream.ReadSingle(inPad: inPad);
        float y = stream.ReadSingle();
        return new Vector2(x, y);
    }

    public static void WriteVector2(this DataStream stream, Vector2 value, bool inPad = false)
    {
        if (inPad)
        {
            stream.Pad(4);
        }
        stream.WriteSingle(value.X);
        stream.WriteSingle(value.Y);
    }

    public static Vector3 ReadVector3(this DataStream stream, bool inPad = false)
    {
        float x = stream.ReadSingle(inPad: inPad);
        float y = stream.ReadSingle();
        float z = stream.ReadSingle();
        return new Vector3(x, y, z);
    }

    public static void WriteVector3(this DataStream stream, Vector3 value, bool inPad = false)
    {
        if (inPad)
        {
            stream.Pad(4);
        }
        stream.WriteSingle(value.X);
        stream.WriteSingle(value.Y);
        stream.WriteSingle(value.Z);
    }

    public static Vector4 ReadVector4(this DataStream stream, bool inPad = false)
    {
        float x = stream.ReadSingle(inPad: inPad);
        float y = stream.ReadSingle();
        float z = stream.ReadSingle();
        float w = stream.ReadSingle();
        return new Vector4(x, y, z, w);
    }

    public static void WriteVector4(this DataStream stream, Vector4 value, bool inPad = false)
    {
        if (inPad)
        {
            stream.Pad(4);
        }
        stream.WriteSingle(value.X);
        stream.WriteSingle(value.Y);
        stream.WriteSingle(value.Z);
        stream.WriteSingle(value.W);
    }

    public static BoundingBox ReadAabb(this DataStream stream, bool inPad = false)
    {
        if (inPad)
        {
            stream.Pad(16);
        }
        return new BoundingBox(stream.ReadVector4(), stream.ReadVector4());
    }

    public static void WriteAabb(this DataStream stream, BoundingBox value, bool inPad = false)
    {
        if (inPad)
        {
            stream.Pad(16);
        }
        stream.WriteVector4(value.Min);
        stream.WriteVector4(value.Max);
    }
}