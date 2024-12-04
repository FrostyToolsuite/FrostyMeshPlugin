using FrostyMeshPlugin.Cloth.Interfaces;

namespace FrostyMeshPlugin.Cloth.Classes;

public class AnchorConstraintSet : IBinarySerializable
{
    public int Version { get; set; }

    public List<float> Constraints { get; } = new();

    public void Deserialize(BinaryStream inStream)
    {
        int count = inStream.ReadInt32();
        for (int i = 0; i < ((count + 3) & ~3); i++)
        {
            float constraint = inStream.ReadSingle();
            if (i < count)
            {
                Constraints.Add(constraint);
            }
        }
    }

    public void Serialize(BinaryStream inStream)
    {
        inStream.WriteInt32(Constraints.Count);
        for (int i = 0; i < ((Constraints.Count + 3) & ~3); i++)
        {
            if (i < Constraints.Count)
            {
                inStream.WriteSingle(Constraints[i]);
            }
            else
            {
                inStream.WriteSingle(1.0f);
            }
        }
    }
}