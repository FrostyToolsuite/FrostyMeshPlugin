using FrostyMeshPlugin.Cloth.Interfaces;

namespace FrostyMeshPlugin.Cloth.Classes;

public class LraSetParams : IBinarySerializable
{
    public int Version { get; set; }

    public bool Unk1 { get; set; } = true;
    public float LraFactor { get; set; }
    public float LraExtensionFactor { get; set; }

    public void Deserialize(BinaryStream inStream)
    {
        Unk1 = inStream.ReadBoolean();
        LraFactor = inStream.ReadSingle();
        LraExtensionFactor = inStream.ReadSingle();
    }

    public void Serialize(BinaryStream inStream)
    {
        inStream.WriteBoolean(Unk1);
        inStream.WriteSingle(LraFactor);
        inStream.WriteSingle(LraExtensionFactor);
    }
}