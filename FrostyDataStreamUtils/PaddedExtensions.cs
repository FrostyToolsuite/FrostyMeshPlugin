using System;
using Frosty.Sdk.IO;

namespace FrostyDataStreamUtils;

public static class PaddedExtensions
{
    public static short ReadInt16(this DataStream stream, Endian inEndian = Endian.Little, bool inPad = true)
    {
        if (inPad)
        {
            stream.Pad(sizeof(short));
        }

        return stream.ReadInt16(inEndian);
    }

    public static ushort ReadUInt16(this DataStream stream, Endian inEndian = Endian.Little, bool inPad = true)
    {
        return (ushort)stream.ReadInt16(inEndian, inPad);
    }

    public static int ReadInt32(this DataStream stream, Endian inEndian = Endian.Little, bool inPad = true)
    {
        if (inPad)
        {
            stream.Pad(sizeof(int));
        }

        return stream.ReadInt32(inEndian);
    }

    public static uint ReadUInt32(this DataStream stream, Endian inEndian = Endian.Little, bool inPad = true)
    {
        return (uint)stream.ReadInt32(inEndian, inPad);
    }

    public static long ReadInt64(this DataStream stream, Endian inEndian = Endian.Little, bool inPad = true)
    {
        if (inPad)
        {
            stream.Pad(sizeof(long));
        }

        return stream.ReadInt64(inEndian);
    }

    public static ulong ReadUInt64(this DataStream stream, Endian inEndian = Endian.Little, bool inPad = true)
    {
        return (ulong)stream.ReadInt64(inEndian, inPad);
    }

    public static unsafe Half ReadHalf(this DataStream stream, Endian inEndian = Endian.Little, bool inPad = true)
    {
        if (inPad)
        {
            stream.Pad(sizeof(Half));
        }

        return stream.ReadHalf(inEndian);
    }

    public static float ReadSingle(this DataStream stream, Endian inEndian = Endian.Little, bool inPad = true)
    {
        if (inPad)
        {
            stream.Pad(sizeof(float));
        }

        return stream.ReadSingle(inEndian);
    }

    public static double ReadDouble(this DataStream stream, Endian inEndian = Endian.Little, bool inPad = true)
    {
        if (inPad)
        {
            stream.Pad(sizeof(double));
        }

        return stream.ReadDouble(inEndian);
    }

    public static Guid ReadGuid(this DataStream stream, Endian inEndian = Endian.Little, bool inPad = true)
    {
        if (inPad)
        {
            stream.Pad(sizeof(uint));
        }

        return stream.ReadGuid(inEndian);
    }
}