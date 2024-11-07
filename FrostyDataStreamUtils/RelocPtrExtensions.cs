using System;
using System.Collections.Generic;
using Frosty.Sdk.IO;

namespace FrostyDataStreamUtils;

public static class RelocPtrExtension
{
    private static readonly Dictionary<string, long> s_mapping = new();
    private static readonly HashSet<long> s_table = new();

    public static void WriteRelocPtr(this DataStream stream, string inName)
    {
        stream.Pad(4);
        if (!s_mapping.TryAdd(inName, stream.Position))
        {
            throw new ArgumentException("RelocPtr already reserved: " + inName);
        }

        s_table.Add(stream.Position);
        stream.WriteInt64(0);
    }

    public static void AddRelocData(this DataStream stream, string inName)
    {
        if (!s_mapping.Remove(inName, out long jump))
        {
            throw new ArgumentException("RelocPtr is not reserved: " + inName);
        }

        long value = stream.Position;

        stream.StepIn(jump);
        stream.WriteInt64(value);
        stream.StepOut();
    }

    public static void WriteRelocTable(this DataStream stream)
    {
        stream.Pad(4);
        foreach (long offset in s_table)
        {
            stream.WriteUInt32((uint)offset);
        }
        s_table.Clear();
    }

    public static bool ReadRelocPtr(this DataStream stream, Action<DataStream>? inReadFunc, Action<DataStream>? inPreJumpFunc = null)
    {
        stream.Pad(4);
        long offset = stream.ReadInt64();
        inPreJumpFunc?.Invoke(stream);
        if (offset <= 0)
        {
            return false;
        }
        stream.StepIn(offset);
        inReadFunc?.Invoke(stream);
        stream.StepOut();
        return true;
    }
}