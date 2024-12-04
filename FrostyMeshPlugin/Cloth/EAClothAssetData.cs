using System;
using Frosty.Sdk.IO;
using Frosty.Sdk.Resources;
using FrostyMeshPlugin.Cloth.Classes;
using FrostyMeshPlugin.Cloth.Interfaces;

namespace FrostyMeshPlugin.Cloth;

public class EAClothAssetData : Resource, IBinarySerializable
{
    public int Version { get; set; }

    public ClothDefinition ClothDefinition { get; } = new();

    public override void Deserialize(DataStream inStream, ReadOnlySpan<byte> inResMeta)
    {
        // meta
        // u32 size
        // u32 unused[3]

        BinaryStream stream = new(inStream);
        stream.StartRead();
        stream.Deserialize(this);
    }

    public override void Serialize(DataStream stream, Span<byte> resMeta)
    {
        throw new NotImplementedException();
    }

    public void Deserialize(BinaryStream inStream)
    {
        inStream.Deserialize(ClothDefinition);
    }

    public void Serialize(BinaryStream inStream)
    {
        throw new NotImplementedException();
    }
}