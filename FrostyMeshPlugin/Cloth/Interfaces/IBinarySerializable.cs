namespace FrostyMeshPlugin.Cloth.Interfaces;

public interface IBinarySerializable
{
    public int Version { get; set; }

    public void Deserialize(BinaryStream inStream);

    public void Serialize(BinaryStream inStream);
}