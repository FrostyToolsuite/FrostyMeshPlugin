using System;
using FrostyMeshPlugin.Cloth.Interfaces;

namespace FrostyMeshPlugin.Cloth.Structs;

public struct Vector3 : IBinarySerializable
{
    public int Version { get; set; } = 1;

    public float X, Y, Z;

    public Vector3()
    {
    }

    public void Deserialize(BinaryStream inStream)
    {
        X = inStream.ReadSingle();
        Y = inStream.ReadSingle();
        Z = inStream.ReadSingle();
    }

    public void Serialize(BinaryStream inStream)
    {
        throw new NotImplementedException();
    }
}