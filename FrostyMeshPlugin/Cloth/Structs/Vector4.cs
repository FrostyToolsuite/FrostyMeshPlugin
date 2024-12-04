using FrostyMeshPlugin.Cloth.Interfaces;

namespace FrostyMeshPlugin.Cloth.Structs;

public struct Vector4 : IBinarySerializable
{
    public int Version { get; set; } = 1;

    public float X, Y, Z, W;

    public Vector4()
    {
    }

    public Vector4(float x, float y, float z, float w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public void Deserialize(BinaryStream inStream)
    {
        X = inStream.ReadSingle();
        Y = inStream.ReadSingle();
        Z = inStream.ReadSingle();
        W = inStream.ReadSingle();
    }

    public void Serialize(BinaryStream inStream)
    {
        inStream.WriteSingle(X);
        inStream.WriteSingle(Y);
        inStream.WriteSingle(Z);
        inStream.WriteSingle(W);
    }
}