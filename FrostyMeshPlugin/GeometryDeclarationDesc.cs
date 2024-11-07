using System.Text;
using Frosty.Sdk;

namespace FrostyMeshPlugin;

public enum VertexElementUsage : byte
{
    Unknown = 0x00,
    Pos = 0x01,
    BoneIndices = 0x02,
    BoneIndices2 = 0x03,
    BoneWeights = 0x04,
    BoneWeights2 = 0x05,
    Normal = 0x06,
    Tangent = 0x07,
    Binormal = 0x08,
    BinormalSign = 0x09,
    WorldTrans1 = 0x0A,
    WorldTrans2 = 0x0B,
    WorldTrans3 = 0x0C,
    InstanceId = 0x0D,
    InstanceUserData0 = 0x0E,
    PrevInstanceUserData0 = 0x0F,
    InstanceUserData1 = 0x10,
    PrevWorldTrans1 = 0x11,
    PrevWorldTrans2 = 0x12,
    PrevWorldTrans3 = 0x13,
    XenonIndex = 0x14,
    XenonBarycentric = 0x15,
    XenonQuadID = 0x16,
    Index = 0x17,
    ViewIndex = 0x18,
    Color0 = 0x1E,
    Color1 = 0x1F,
    TexCoord0 = 0x21,
    TexCoord1 = 0x22,
    TexCoord2 = 0x23,
    TexCoord3 = 0x24,
    TexCoord4 = 0x25,
    TexCoord5 = 0x26,
    TexCoord6 = 0x27,
    TexCoord7 = 0x28,
    DisplacementMapTexCoord = 0x29,
    RadiosityTexCoord = 0x2A,
    VisInfo = 0x2B,
    SpriteSize = 0x2C,
    PackedTexCoord0 = 0x2D,
    PackedTexCoord1 = 0x2E,
    PackedTexCoord2 = 0x2F,
    PackedTexCoord3 = 0x30,
    ClipDistance0 = 0x31,
    ClipDistance1 = 0x32,
    SubMaterialIndex = 0x33,
    TangentSpace = 0x34,
    BranchInfo = 0x3C,
    PosAndScale = 0x3D,
    Rotation = 0x3E,
    SpriteSizeAndUv = 0x3F,
    FadePos = 0x5A,
    SpawnTime = 0x5B,
    RegionIds = 0x64,
    BlendWeights = 0x65,
    PosAndSoftMul = 0x96,
    Alpha = 0x97,
    Misc0 = 0x98,
    Misc1 = 0x99,
    Misc2 = 0x9A,
    Misc3 = 0x9B,
    LeftAndRotation = 0x9C,
    UpAndNormalBlend = 0x9D,
    Hl2BasisL0 = 0x9E,
    Hl2BasisL1 = 0x9F,
    Hl2BasisL3 = 0xA0,
    PosAndRejectCulling = 0xA1,
    Shadow = 0xA2,
    CustomParams = 0xA3,
    PatchUv = 0xB4,
    Height = 0xB5,
    MaskUVs0 = 0xB6,
    MaskUVs1 = 0xB7,
    MaskUVs2 = 0xB8,
    MaskUVs3 = 0xB9,
    UserMasks = 0xBA,
    HeightfieldUv = 0xBB,
    MaskUv = 0xBC,
    GlobalColorUv = 0xBD,
    HeightfieldPixelSizeAndAspect = 0xBE,
    WorldPositionXz = 0xBF,
    TerrainTextureNodeUv = 0xC0,
    ParentTerrainTextureNodeUv = 0xC1,
    DeformationIndex = 0xCD,
    DeformationWeight = 0xCE,
    DeformationPosition = 0xCF,
    Delta = 0xD0,
    ElementIndex = 0xD1,
    Uv01 = 0xD2,
    WorldPos = 0xD3,
    EyeVector = 0xD4,
    LightParams1 = 0xDC,
    LightParams2 = 0xDD,
    LightSubParams = 0xDE,
    LightSideVector = 0xDF,
    LightInnerAndOuterAngle = 0xE0,
    LightDir = 0xE1,
    LightMatrix1 = 0xE2,
    LightMatrix2 = 0xE3,
    LightMatrix3 = 0xE4,
    LightMatrix4 = 0xE5,
    Custom = 0xE6,
    DestructionMaskDistance = 0xF0,
    DestructionMaskTexCoord = 0xF1,
    VertIndex = 0xFA,
}

public enum VertexElementFormat : byte
{
    None = 0x00,
    Float = 0x01,
    Float2 = 0x02,
    Float3 = 0x03,
    Float4 = 0x04,
    Half = 0x05,
    Half2 = 0x06,
    Half3 = 0x07,
    Half4 = 0x08,
    UByteN = 0x32,
    Byte4 = 0x0A,
    Byte4N = 0x0B,
    UByte4 = 0x0C,
    UByte4N = 0x0D,
    Short = 0x0E,
    Short2 = 0x0F,
    Short3 = 0x10,
    Short4 = 0x11,
    ShortN = 0x12,
    Short2N = 0x13,
    Short3N = 0x14,
    Short4N = 0x15,
    UShort2 = 0x16,
    UShort4 = 0x17,
    UShort2N = 0x18,
    UShort4N = 0x19,
    Int = 0x1A,
    Int2 = 0x1B,
    Int3 = 0x33,
    Int4 = 0x1C,
    IntN = 0x1D,
    Int2N = 0x1E,
    Int4N = 0x1F,
    UInt = 0x20,
    UInt2 = 0x21,
    UInt3 = 0x34,
    UInt4 = 0x22,
    UIntN = 0x23,
    UInt2N = 0x24,
    UInt4N = 0x25,
    Comp3_10_10_10 = 0x26,
    Comp3N_10_10_10 = 0x27,
    UComp3_10_10_10 = 0x28,
    UComp3N_10_10_10 = 0x29,
    Comp3_11_11_10 = 0x2A,
    Comp3N_11_11_10 = 0x2B,
    UComp3_11_11_10 = 0x2C,
    UComp3N_11_11_10 = 0x2D,
    Comp4_10_10_10_2 = 0x2E,
    Comp4N_10_10_10_2 = 0x2F,
    UComp4_10_10_10_2 = 0x30,
    UComp4N_10_10_10_2 = 0x31,
}

public enum VertexElementClassification
{
    PerVertex = 0,
    PerInstance = 1,
    Index = 2
}

public struct GeometryDeclarationDesc
{
    public const int MaxElements = 16;
    public static readonly int MaxStreams = ProfilesLibrary.FrostbiteVersion <= "2014.4.11" ? 4 : ProfilesLibrary.FrostbiteVersion <= "2014.4.17" ? 6 : ProfilesLibrary.FrostbiteVersion < "2016.4" ? 8 : 16;

    public struct Element
    {
        public VertexElementUsage Usage;
        public VertexElementFormat Format;
        public byte Offset;
        public byte StreamIndex;
    }

    public struct Stream
    {
        public byte VertexStride;
        public VertexElementClassification Classification;
    }

    public Element[] Elements;
    public Stream[] Streams;
    public byte ElementCount;
    public byte StreamCount;

    public override string ToString()
    {
        StringBuilder sb = new();
        
        foreach (Element element in Elements)
        {
            if (element.Usage == VertexElementUsage.Unknown)
            {
                continue;
            }
            
            sb.AppendLine($"Element {element.Usage}: {element.Format}");
        }

        return sb.ToString();
    }
}