using System;
using Frosty.Sdk.IO;
using FrostyMeshPlugin.Cloth.Interfaces;

namespace FrostyMeshPlugin.Cloth;

public class BinaryStream
{
    private DataStream m_stream;
    private Endian m_endian;

    public BinaryStream(DataStream inStream)
    {
        m_stream = inStream;
    }

    public void StartRead()
    {
        uint magic = m_stream.ReadUInt32(Endian.Big);
        if (magic != 0x424E5259) // BNRY
        {
            throw new Exception("Not a valid binary stream");
        }

        int version = m_stream.ReadInt32(Endian.Big);
        if (version != 2)
        {
            throw new NotImplementedException("Binary stream version not implemented");
        }

        uint endianTag = m_stream.ReadUInt32(Endian.Big);
        if (endianTag == 0x4C544C45) // LTLE
        {
            m_endian = Endian.Little;
        }
        else if (endianTag == 0x42494745) // BIGE
        {
            m_endian = Endian.Big;
        }
        else
        {
            throw new Exception("Invalid endian tag");
        }
    }

    public T Deserialize<T>() where T : IBinarySerializable, new()
    {
        int version = m_stream.ReadInt32(m_endian);
        T retVal = new() { Version = version };
        retVal.Deserialize(this);
        return retVal;
    }

    public byte ReadByte() => m_stream.ReadByte();
    public sbyte ReadSByte() => m_stream.ReadSByte();
    public bool ReadBoolean() => m_stream.ReadBoolean(); // 0xFF or 0x00
    public short ReadIn16() => m_stream.ReadInt16(m_endian);
    public ushort ReadUInt16() => m_stream.ReadUInt16(m_endian);
    public int ReadInt32() => m_stream.ReadInt32(m_endian);
    public uint ReadUInt32() => m_stream.ReadUInt32(m_endian);
    public long ReadInt64() => m_stream.ReadInt64(m_endian);
    public ulong ReadUInt64() => m_stream.ReadUInt64(m_endian);
    public float ReadSingle() => m_stream.ReadSingle(m_endian);
    public double ReadDouble() => m_stream.ReadDouble(m_endian);
}