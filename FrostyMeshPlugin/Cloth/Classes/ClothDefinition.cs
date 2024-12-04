using System;
using FrostyMeshPlugin.Cloth.Interfaces;

namespace FrostyMeshPlugin.Cloth.Classes;

public class ClothDefinition : IBinarySerializable
{
    public int Version { get; set; }

    public ClothParameters DefaultParameters { get; private set; }
    public int ClothPointCount { get; private set; }
    public int Unk2 { get; private set; } = 0;
    public ClothPoints AuthoredAnchorPoints { get; private set; }
    public ClothPoints AuthoredAnchorNormals { get; private set; }
    public ConstraintSet StructureContraints { get; } = new();
    public ConstraintSet? ShearContraints { get; private set; }
    public ConstraintSet FoldContraints { get; } = new();
    public AnchorConstraintSet AnchorConstraints { get; private set; }
    public AnchorConstraintSet AnchorPlaneConstraints { get; private set; }
    public uint AnchorCount { get; private set; } = 4;
    public Vector<ConstraintSet>? LraConstraintSets { get; private set; }
    public Vector<LraSetParams>? LraSetParams { get; private set; }

    public void Deserialize(BinaryStream inStream)
    {
        DefaultParameters = inStream.Deserialize<ClothParameters>();

        ClothPointCount = inStream.ReadInt32();
        if (Version > 1)
        {
            Unk2 = inStream.ReadInt32();
        }

        AuthoredAnchorPoints = inStream.Deserialize<ClothPoints>();
        AuthoredAnchorNormals = inStream.Deserialize<ClothPoints>();
        inStream.Deserialize(StructureContraints);
        if (Version > 3)
        {
            ShearContraints = inStream.Deserialize<ConstraintSet>();
        }
        inStream.Deserialize(FoldContraints);
        if (Version < 9)
        {
            ConstraintSet anchorConstraints = inStream.Deserialize<ConstraintSet>();
            foreach (ConstraintDesc desc in anchorConstraints.Constraints)
            {
                AnchorConstraints.Constraints[desc.Unk1] = desc.Unk3;
            }
            ConstraintSet anchorPlaneConstraints = inStream.Deserialize<ConstraintSet>();
            foreach (ConstraintDesc desc in anchorPlaneConstraints.Constraints)
            {
                AnchorPlaneConstraints.Constraints[desc.Unk1] = desc.Unk3;
            }
        }
        else
        {
            AnchorConstraints = inStream.Deserialize<AnchorConstraintSet>();
            AnchorPlaneConstraints = inStream.Deserialize<AnchorConstraintSet>();
            AnchorCount = inStream.ReadUInt32();
        }

        if (Version < 0x12)
        {
            var lraConstraints = inStream.Deserialize<ConstraintSet>();
        }
        else if (Version < 0x17)
        {
            var anchorRootLRAConstraints = inStream.Deserialize<ConstraintSet>();
            var clothRootLRAConstraints = inStream.Deserialize<ConstraintSet>();
        }
        else
        {
            LraConstraintSets = inStream.Deserialize<Vector<ConstraintSet>>();
            LraSetParams = inStream.Deserialize<Vector<LraSetParams>>();
        }
    }

    public void Serialize(BinaryStream inStream)
    {
        throw new NotImplementedException();
    }
}