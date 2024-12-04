using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Frosty.Sdk;
using Frosty.Sdk.IO;
using Frosty.Sdk.Profiles;
using Frosty.Sdk.Resources;
using Frosty.Sdk.Utils;
using FrostyDataStreamUtils;
using FrostyMeshPlugin.Utils;

namespace FrostyMeshPlugin.Resources;

public enum MeshType
{
    Rigid,
    Skinned,
    Composite
}

public class MeshSet : Resource
{

    [Flags]
    private enum MeshSetFlags : uint
    {

    }

    public IEnumerable<Mesh> Meshes => m_meshes;

    private const int c_maxMeshCount = 6; // MeshLimits.MaxMeshLodCount

    public static readonly bool IsFifa = ProfilesLibrary.IsLoaded(ProfileVersion.Fifa17, ProfileVersion.Fifa18,
        ProfileVersion.Fifa19, ProfileVersion.Fifa20, ProfileVersion.Fifa21, ProfileVersion.Fifa22,
        ProfileVersion.Fifa23);

    private static readonly bool s_isMadden = ProfilesLibrary.IsLoaded(ProfileVersion.Madden19, ProfileVersion.Madden20,
        ProfileVersion.Madden21, ProfileVersion.Madden22, ProfileVersion.Madden23, ProfileVersion.Madden24, ProfileVersion.Madden25);

    private BoundingBox m_boundingBox;
    public string Name { get; private set; }
    public string ShortName { get; private set; }
    private uint m_nameHash;
    private MeshType m_meshType;
    private byte m_unk1;
    private ushort[] m_lodFadeDistanceFactors = new ushort[c_maxMeshCount * 2 - 1];
    private uint[] m_unk2 = new uint[4];
    private MeshSetFlags m_flags;
    private byte m_shaderDrawOrder;
    private byte m_shaderDrawPostFlush;
    private byte m_shaderDrawOrderUserSlot;
    private short m_shaderDrawOrderSubOrder;
    private ushort[] m_subsetStartIndices = new ushort[c_maxMeshCount];
    private ushort m_boneOrPartCount;
    private List<ushort> m_cullBoxBoneIndices = new();
    private List<BoundingBox> m_cullBoundingBoxes = new();
    private List<Matrix4x4> m_partTransforms = new();
    private List<BoundingBox> m_partBoundingBoxes = new();
    public Block<byte>? InlineData { get; private set; }

    private readonly List<Mesh> m_meshes = new(c_maxMeshCount);

    public override void Deserialize(DataStream inStream, ReadOnlySpan<byte> inResMeta)
    {
        int headerSize, inlineVertexDataSize, relocTableSize, meshSetSize, subSetSize;
        if (ProfilesLibrary.IsLoaded(ProfileVersion.DragonAgeVeilguard))
        {
            headerSize = BinaryPrimitives.ReadInt32LittleEndian(inResMeta[0..]); // size of meshset + meshes + subset
            relocTableSize = BinaryPrimitives.ReadInt32LittleEndian(inResMeta[4..]); // size of reloc table
            inlineVertexDataSize = BinaryPrimitives.ReadInt32LittleEndian(inResMeta[8..]); // size of vertex data inside this res
            meshSetSize = inStream.ReadInt32();
            uint meshSize = inStream.ReadUInt32();
            subSetSize = inStream.ReadInt32();
            inStream.Pad(16);

            Block<byte> buffer = new((int)(inStream.Length - inStream.Position));
            inStream.ReadExactly(buffer);

            inStream = new BlockStream(buffer);
        }
        else
        {
            headerSize = BinaryPrimitives.ReadInt32LittleEndian(inResMeta[0..]); // size of meshset + meshes + subset
            inlineVertexDataSize = BinaryPrimitives.ReadInt32LittleEndian(inResMeta[4..]); // size of vertex data inside this res
            relocTableSize = BinaryPrimitives.ReadInt32LittleEndian(inResMeta[8..]); // size of reloc table
            meshSetSize = BinaryPrimitives.ReadUInt16LittleEndian(inResMeta[12..]); // size of meshset
            subSetSize = BinaryPrimitives.ReadUInt16LittleEndian(inResMeta[14..]); // size of subset
        }

        m_boundingBox = inStream.ReadAabb();

        for (int i = 0; i < c_maxMeshCount; i++)
        {
            inStream.ReadRelocPtr(stream =>
            {
                Mesh mesh = new();
                mesh.Deserialize(stream, subSetSize);
                m_meshes.Add(mesh);
            });
        }

        if (ProfilesLibrary.FrostbiteVersion > "2014.1")
        {
            Debug.Assert(!inStream.ReadRelocPtr(null));
        }

        Name = inStream.ReadString();
        ShortName = inStream.ReadString();
        m_nameHash = inStream.ReadUInt32(inPad: true);

        if (ProfilesLibrary.FrostbiteVersion >= "2021.1.1")
        {
            m_meshType = (MeshType)inStream.ReadByte();
            m_unk1 = inStream.ReadByte();
        }
        else
        {
            m_meshType = (MeshType)inStream.ReadInt32(inPad: true);
        }

        if (ProfilesLibrary.FrostbiteVersion > "2014.4.11")
        {
            for (int i = 0; i < m_lodFadeDistanceFactors.Length; i++)
            {
                m_lodFadeDistanceFactors[i] = inStream.ReadUInt16(inPad: true);
            }
        }

        if (ProfilesLibrary.FrostbiteVersion >= "2021.1.1")
        {
            for (int i = 0; i < (ProfilesLibrary.IsLoaded(ProfileVersion.DragonAgeVeilguard) || ProfilesLibrary.FrostbiteVersion >= "2023.1.1" ? 3 : 4); i++)
            {
                m_unk2[i] = inStream.ReadUInt32(inPad: true);
            }
        }

        if (IsFifa || s_isMadden)
        {
            m_flags = (MeshSetFlags)inStream.ReadInt64(inPad: true);
        }
        else
        {
            m_flags = (MeshSetFlags)inStream.ReadUInt32(inPad: true);
        }

        if (ProfilesLibrary.IsLoaded(ProfileVersion.DragonAgeVeilguard))
        {
            inStream.ReadInt32();
            inStream.ReadByte();
        }

        if (ProfilesLibrary.FrostbiteVersion >= "2015.4")
        {
            m_shaderDrawOrder = inStream.ReadByte();
            if (s_isMadden)
            {
                m_shaderDrawPostFlush = inStream.ReadByte();
            }
            m_shaderDrawOrderUserSlot = inStream.ReadByte();
            m_shaderDrawOrderSubOrder = inStream.ReadInt16(inPad: true);
        }

        ushort meshCount = inStream.ReadUInt16(inPad: true);
        ushort subsetCount = inStream.ReadUInt16(inPad: true);

        Debug.Assert(m_meshes.Count == meshCount, "Mesh count doesnt match");

        if (ProfilesLibrary.IsLoaded(ProfileVersion.Madden22) || ProfilesLibrary.FrostbiteVersion >= "2021.1.1")
        {
            for (int i = 0; i < c_maxMeshCount; i++)
            {
                m_subsetStartIndices[i] = inStream.ReadUInt16(inPad: true);
            }
        }

        if (ProfilesLibrary.IsLoaded(ProfileVersion.DragonAgeVeilguard))
        {
            uint unk = inStream.ReadUInt32(inPad: true);
            inStream.ReadRelocPtr(null); // LinearTransforms for each bone
            inStream.ReadRelocPtr(null);
        }

        if (ProfilesLibrary.FrostbiteVersion >= "2015.4")
        {
            switch (m_meshType)
            {
                case MeshType.Rigid:
                    // nothing to do
                    break;
                case MeshType.Skinned:
                    m_boneOrPartCount = inStream.ReadUInt16(inPad: true);
                    ushort cullBoxCount = inStream.ReadUInt16(inPad: true);

                    if (cullBoxCount <= 0)
                    {
                        break;
                    }

                    inStream.ReadRelocPtr(stream =>
                    {
                        for (int i = 0; i < cullBoxCount; i++)
                        {
                            m_cullBoxBoneIndices.Add(stream.ReadUInt16(inPad: true));
                        }
                    });

                    inStream.ReadRelocPtr(stream =>
                    {
                        for (int i = 0; i < cullBoxCount; i++)
                        {
                            m_cullBoundingBoxes.Add(stream.ReadAabb());
                        }
                    });

                    break;
                case MeshType.Composite:
                    m_boneOrPartCount = inStream.ReadUInt16(inPad: true);
                    inStream.Position += 2; // union padding

                    if (m_boneOrPartCount <= 0)
                    {
                        break;
                    }

                    // if the parts only have an Identity transform then they are not stored at all
                    inStream.ReadRelocPtr(stream =>
                    {
                        for (int i = 0; i < m_boneOrPartCount; i++)
                        {
                            m_partTransforms.Add(stream.ReadLinearTransform());
                        }
                    });

                    inStream.ReadRelocPtr(stream =>
                    {
                        for (int i = 0; i < m_boneOrPartCount; i++)
                        {
                            m_partBoundingBoxes.Add(stream.ReadAabb());
                        }
                    });

                    break;
            }
        }

        inStream.Pad(16);

        Debug.Assert(inStream.Position == meshSetSize, "Didnt read MeshSet correctly");

        if (inlineVertexDataSize != 0)
        {
            inStream.Position = headerSize;
            InlineData = new Block<byte>(inlineVertexDataSize);
            inStream.ReadExactly(InlineData);
        }

        if (ProfilesLibrary.IsLoaded(ProfileVersion.DragonAgeVeilguard))
        {
            inStream.Dispose();
        }
    }

    public override void Serialize(DataStream stream, Span<byte> resMeta)
    {
        throw new NotImplementedException();
    }
}