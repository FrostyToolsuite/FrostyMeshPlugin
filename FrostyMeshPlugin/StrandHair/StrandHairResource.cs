using System.Buffers.Binary;
using System.Diagnostics;
using Frosty.Sdk.IO;
using Frosty.Sdk.Resources;

namespace FrostyMeshPlugin.StrandHair;

public class StrandHairResource : Resource
{
    public List<(short, short, short, short)>? Unk1;

    public override void Deserialize(DataStream inStream, ReadOnlySpan<byte> inResMeta)
    {
        int relocTableLength = BinaryPrimitives.ReadInt32LittleEndian(inResMeta[4..]);
        Debug.Assert(relocTableLength != 52 * 4);

        int unk1Count = inStream.ReadInt32();
        Unk1 = new List<(short, short, short, short)>(unk1Count);
        inStream.StepIn(inStream.ReadInt64());
        for (int i = 0; i < unk1Count; i++)
        {
            Unk1.Add((inStream.ReadInt16(), inStream.ReadInt16(), inStream.ReadInt16(), inStream.ReadInt16()));
        }
        inStream.StepOut();
    }

    public override void Serialize(DataStream inStream, Span<byte> resMeta)
    {
        throw new NotImplementedException();
    }
}