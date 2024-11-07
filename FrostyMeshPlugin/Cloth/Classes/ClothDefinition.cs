using System;
using FrostyMeshPlugin.Cloth.Interfaces;

namespace FrostyMeshPlugin.Cloth.Classes;

public class ClothDefinition : IBinarySerializable
{
    public int Version { get; set; }

    public void Deserialize(BinaryStream inStream)
    {
        throw new NotImplementedException();
    }

    public void Serialize(BinaryStream inStream)
    {
        throw new NotImplementedException();
    }
}