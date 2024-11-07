using System;
using FrostyMeshPlugin.Cloth.Interfaces;
using FrostyMeshPlugin.Cloth.Structs;

namespace FrostyMeshPlugin.Cloth.Classes;

public class ClothParameters : IBinarySerializable
{
    public int Version { get; set; }

    public float StructuralFactor { get; set; }
    public float FoldFactor { get; set; }
    public float LRAFactor { get; set; }
    public Vector3 Gravity { get; set; }
    public float AnchorFactor { get; set; }
    public float PenetrationFactor { get; set; }
    public float GroundFriction { get; set; } = 0.0f;
    public float BodyFriction { get; set; } = 0.0f;
    public float ShearFactor { get; set; } = 1.0f;
    public float AnchorScale { get; set; } = 1.0f;
    public float PenetrationScale { get; set; } = 1.0f;
    public float WindLiftToDragRation { get; set; } = 1.0f;

    public void Deserialize(BinaryStream inStream)
    {
        StructuralFactor = inStream.ReadSingle();
        FoldFactor = inStream.ReadSingle();

        if (Version < 0x10)
        {
            inStream.ReadInt32();
        }

        LRAFactor = inStream.ReadSingle();
        Gravity = inStream.Deserialize<Vector3>();

        if (Version < 0x10)
        {
            inStream.ReadInt32();
        }

        AnchorFactor = inStream.ReadSingle();
        PenetrationFactor = inStream.ReadSingle();

        if (Version > 1)
        {

        }
    }

    public void Serialize(BinaryStream inStream)
    {
        throw new NotImplementedException();
    }
}