using FrostyMeshPlugin.Cloth.Interfaces;

namespace FrostyMeshPlugin.Cloth.Classes;

public class Vector<T> : IBinarySerializable where T : IBinarySerializable, new()
{
    public int Version { get; set; }
    private List<T> m_internal = new();

    public void Deserialize(BinaryStream inStream)
    {
        int count = inStream.ReadInt32();
        for (int i = 0; i < ((count + 3) & ~3); i++)
        {
            T item = new() { Version = Version };
            item.Deserialize(inStream);
            if (i < count)
            {
                m_internal.Add(item);
            }
        }
    }

    public void Serialize(BinaryStream inStream)
    {
        inStream.WriteInt32(m_internal.Count);
        for (int i = 0; i < ((m_internal.Count + 3) & ~3); i++)
        {
            if (i < m_internal.Count)
            {
                m_internal[i].Serialize(inStream);
            }
            else
            {
                new T().Serialize(inStream);
            }
        }
    }
}