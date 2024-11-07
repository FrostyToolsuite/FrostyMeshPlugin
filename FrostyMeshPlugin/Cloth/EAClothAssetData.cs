using System;
using Frosty.Sdk.IO;
using Frosty.Sdk.Resources;

namespace FrostyMeshPlugin.Cloth;

public class EAClothAssetData : Resource
{
    public override void Deserialize(DataStream inStream, ReadOnlySpan<byte> inResMeta)
    {
        // meta
        // u32 size
        // u32 unused[3]

        BinaryStream stream = new(inStream);
        stream.StartRead();
    }

    public override void Serialize(DataStream stream, Span<byte> resMeta)
    {
        throw new NotImplementedException();
    }
}