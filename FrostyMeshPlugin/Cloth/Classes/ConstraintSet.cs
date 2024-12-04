using FrostyMeshPlugin.Cloth.Interfaces;

namespace FrostyMeshPlugin.Cloth.Classes;

public class ConstraintSet : IBinarySerializable
{
    public int Version { get; set; } = 2;

    public List<ConstraintDesc> Constraints { get; } = new();

    public void Deserialize(BinaryStream inStream)
    {
        inStream.DeserializeArray(Constraints, inStream.ReadInt32());
    }

    public void Serialize(BinaryStream inStream)
    {
        throw new NotImplementedException();
    }
}