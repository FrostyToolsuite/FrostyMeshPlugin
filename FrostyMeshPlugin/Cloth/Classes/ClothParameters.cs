using FrostyMeshPlugin.Cloth.Interfaces;
using FrostyMeshPlugin.Cloth.Structs;

namespace FrostyMeshPlugin.Cloth.Classes;

public class ClothParameters : IBinarySerializable
{
    public int Version { get; set; }

    public float StructuralFactor { get; set; }
    public float FoldFactor { get; set; }
    public float LraFactor { get; set; }
    public Vector3 Gravity { get; set; }
    public float AnchorFactor { get; set; }
    public float PenetrationFactor { get; set; }
    public float GroundFriction { get; set; } = 0.0f;
    public float BodyFriction { get; set; } = 0.0f;
    public float ShearFactor { get; set; } = 1.0f;
    public float AnchorScale { get; set; } = 1.0f;
    public float PenetrationScale { get; set; } = 1.0f;
    public float WindLiftToDragRation { get; set; } = 1.0f;
    public bool WindTwoSided { get; set; } = false;
    public float ClothPointRadiiSpring { get; set; } = 0.0f;
    public float AnchorSpringFactor { get; set; } = 0.0f;
    public float HorizontalDamping { get; set; } = 0.0f;
    public float VerticalDamping { get; set; } = 0.0f;
    public float AnchorSpringAttenuation { get; set; } = 0.0f;
    public float SpeedDampingAttenuation { get; set; } = 0.0f;
    public float DistanceDampingAttenuation { get; set; } = 0.0f;
    public bool UseGlobalAnchorSpringFactor { get; set; } = true;
    public bool EnableGroundCollision { get; set; } = true;

    public Vector3 GroundPlanePosition { get; set; } = new(0, 0, 0);
    public Vector3 GroundPlaneNormal { get; set; } = new(0, 1, 0);
    public float LraExtensionFactor { get; set; } = 0.0f;
    public float MediumDensity { get; set; } = 0.0f;
    public float ClothArealDensity { get; set; } = 0.0f;
    public float InitialStrainMultiplier { get; set; } = 0.0f;
    public uint NumStretchPasses { get; set; } = 1;
    public uint NumShearPasses { get; set; } = 1;
    public uint NumFoldPasses { get; set; } = 1;
    public uint NumLraPasses { get; set; } = 1;
    public uint NumAnchorPasses { get; set; } = 1;
    public uint NumPenetrationPasses { get; set; } = 1;
    public uint NumColliderPasses { get; set; } = 1;
    public float StaticCollisionRadius { get; set; } = 0.01f;

    public bool ClothTriangleCollision { get; set; } = false;
    public bool ClothSelfCollision { get; set; } = false;

    public uint NumSubsteps { get; set; } = 1;

    public float SelfCollisionThickness { get; set; } = 0.2f;
    public float SelfCollisionFriction { get; set; } = 0.0f;
    public uint SelfCollisionType { get; set; } = 0;
    public float ContactCapacityMultiplier { get; set; } = 2.0f;
    public bool EnableMaximumContactFriction { get; set; } = true;

    public float MaximumContactFriction { get; set; } = 0.005f;
    public bool UseBdf2 { get; set; } = false;
    public float MaximumCollisionRadius { get; set; } = 0.75f;
    public bool Unk1 { get; set; }
    public float Unk2 { get; set; }
    public uint Unk3 { get; set; }

    public void Deserialize(BinaryStream inStream)
    {
        StructuralFactor = inStream.ReadSingle();
        FoldFactor = inStream.ReadSingle();

        if (Version < 0x10)
        {
            inStream.ReadInt32();
        }

        LraFactor = inStream.ReadSingle();
        Gravity = inStream.Deserialize<Vector3>();

        if (Version < 0x10)
        {
            inStream.ReadInt32();
        }

        AnchorFactor = inStream.ReadSingle();
        PenetrationFactor = inStream.ReadSingle();
        if (Version > 1)
        {
            GroundFriction = inStream.ReadSingle();
        }
        if (Version > 3)
        {
            BodyFriction = inStream.ReadSingle();
        }
        if (Version > 2)
        {
            ShearFactor = inStream.ReadSingle();
        }
        if (Version > 4)
        {
            AnchorScale = inStream.ReadSingle();
            PenetrationScale = inStream.ReadSingle();
        }
        if (Version > 5)
        {
            WindLiftToDragRation = inStream.ReadSingle();
            WindTwoSided = inStream.ReadBoolean();
        }
        if (Version > 6)
        {
            ClothPointRadiiSpring = inStream.ReadSingle();
            AnchorSpringFactor = inStream.ReadSingle();
            HorizontalDamping = inStream.ReadSingle();
            VerticalDamping = inStream.ReadSingle();
            AnchorSpringAttenuation = inStream.ReadSingle();
            SpeedDampingAttenuation = inStream.ReadSingle();
            DistanceDampingAttenuation = inStream.ReadSingle();
        }
        if (Version > 7)
        {
            UseGlobalAnchorSpringFactor = inStream.ReadBoolean();
        }
        if (Version > 8)
        {
            EnableGroundCollision = inStream.ReadBoolean();

            GroundPlanePosition = inStream.Deserialize<Vector3>();
            GroundPlaneNormal = inStream.Deserialize<Vector3>();
        }
        if (Version > 9)
        {
            LraExtensionFactor = inStream.ReadSingle();
            MediumDensity = inStream.ReadSingle();
            ClothArealDensity = inStream.ReadSingle();
            InitialStrainMultiplier = inStream.ReadSingle();
        }
        if (Version > 0xa)
        {
            NumStretchPasses = inStream.ReadUInt32();
            NumShearPasses = inStream.ReadUInt32();
            NumFoldPasses = inStream.ReadUInt32();
            NumLraPasses = inStream.ReadUInt32();
            NumAnchorPasses = inStream.ReadUInt32();
            NumPenetrationPasses = inStream.ReadUInt32();
            NumColliderPasses = inStream.ReadUInt32();
        }
        if (Version > 0xb)
        {
            StaticCollisionRadius = inStream.ReadSingle();

            ClothTriangleCollision = inStream.ReadBoolean();
            ClothSelfCollision = inStream.ReadBoolean();

            NumSubsteps = inStream.ReadUInt32();

            SelfCollisionThickness = inStream.ReadSingle();
            SelfCollisionFriction = inStream.ReadSingle();
            SelfCollisionType = inStream.ReadUInt32();
            ContactCapacityMultiplier = inStream.ReadSingle();
        }
        if (Version > 0xc)
        {
            EnableMaximumContactFriction = inStream.ReadBoolean();

            MaximumContactFriction = inStream.ReadSingle();
        }
        if (Version > 0xd)
        {
            UseBdf2 = inStream.ReadBoolean();
        }
        if (Version > 0xe)
        {
            MaximumCollisionRadius = inStream.ReadSingle();
        }
        if (Version > 0xf)
        {
            Unk1 = inStream.ReadBoolean();
            Unk2 = inStream.ReadSingle();
            Unk3 = inStream.ReadUInt32();
        }
    }

    public void Serialize(BinaryStream inStream)
    {
        throw new NotImplementedException();
    }
}