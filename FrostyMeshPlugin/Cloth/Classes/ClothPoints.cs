using FrostyMeshPlugin.Cloth.Interfaces;
using FrostyMeshPlugin.Cloth.Structs;

namespace FrostyMeshPlugin.Cloth.Classes;

public class ClothPoints : IBinarySerializable
{
    public int Version { get; set; } = 3;

    public uint Unk1 { get; set; } = 0;
    public List<Vector4> Points { get; set; }

    public void Deserialize(BinaryStream inStream)
    {
        int count = inStream.ReadInt32();
        if (Version > 1)
        {
            Unk1 = inStream.ReadUInt32();
        }

        // TODO: i dont think this count is padded
        Points = inStream.DeserializeArray<Vector4>(count);
    }

    public void Serialize(BinaryStream inStream)
    {
        throw new NotImplementedException();
    }
}