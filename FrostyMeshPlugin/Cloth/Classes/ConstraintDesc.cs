using FrostyMeshPlugin.Cloth.Interfaces;

namespace FrostyMeshPlugin.Cloth.Classes;

public class ConstraintDesc : IBinarySerializable
{
    public int Version { get; set; }

    public ushort Unk1 { get; set; } = ushort.MaxValue;
    public ushort Unk2 { get; set; } = ushort.MaxValue;
    public float Unk3 { get; set; } = 1.0f;

    public void Deserialize(BinaryStream inStream)
    {
        Unk1 = inStream.ReadUInt16();
        Unk2 = inStream.ReadUInt16();
        Unk3 = inStream.ReadSingle();
    }

    public void Serialize(BinaryStream inStream)
    {
        inStream.WriteUInt16(Unk1);
        inStream.WriteUInt16(Unk2);
        inStream.WriteSingle(Unk3);
    }
}