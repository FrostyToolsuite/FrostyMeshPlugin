using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Frosty.Sdk;
using Frosty.Sdk.IO;
using Frosty.Sdk.Profiles;
using FrostyDataStreamUtils;
using FrostyMeshPlugin.Utils;

namespace FrostyMeshPlugin.Resources;

public class Mesh
{
    [Flags]
    public enum MeshFlags : uint
    {
        IsBaseLod = 1 << 0,
        StreamInstancingEnable = 1 << 4,
        StreamingEnable = 1 << 6,
        VertexAnimationEnable = 1 << 7,
        Deformation = 1 << 8,
        MultiStreamEnable = 1 << 9,
        SubsetSortingEnable = 1 << 10,
        Inline = 1 << 11,
        AlternateBatchSorting = 1 << 12,
        ProjectedDecalsEnable = 1 << 15,
        ClothEnable = 1 << 16,
        SrvEnable = 1 << 17,
        IsMeshFront = 1 << 28,
        IsDataAvailable = 1 << 29
    }

    public IEnumerable<MeshSubset> Subsets => m_subsets;

    private static readonly int s_maxCategoryCount = ProfilesLibrary.FrostbiteVersion > "2014.1" ? 5 : 4; // TODO: maybe convert into TypeLibrary.Get("MeshSubsetCategory").MeshSubsetCategoryCount

    private static readonly bool s_hasAdjacencyInMesh = ProfilesLibrary.IsLoaded(ProfileVersion.NeedForSpeedRivals,
        ProfileVersion.DragonAgeInquisition, ProfileVersion.Battlefield4, ProfileVersion.PlantsVsZombiesGardenWarfare,
        ProfileVersion.MirrorsEdgeCatalyst, ProfileVersion.Battlefield1, ProfileVersion.StarWarsBattlefrontII,
        ProfileVersion.Battlefield5, ProfileVersion.StarWarsSquadrons);

    private MeshType m_meshType;
    private int m_maxInstances;
    private int m_unkEnum;
    private MeshFlags m_flags;
    public int IndexFormat { get; private set; }
    public int PrimitiveSize => IndexFormat * 2 + 2;
    public uint IndexBufferSize { get; private set; }
    public uint VertexBufferSize { get; private set; }
    public Guid ChunkId { get; private set; }
    public int InlineVertexDataOffset;
    private string m_debugName;
    public string Name { get; set; }
    public string ShortName { get; private set; }
    private uint m_nameHash;
    public List<uint> BoneIndices { get; } = new();
    public List<uint> BoneShortNames { get; } = new();
    public List<BoundingBox> PartBoundingBoxes { get; } = new();
    public List<Matrix4x4> PartTransforms { get; } = new();
    public List<List<int>> PartIndices { get; } = new();

    private List<MeshSubset> m_subsets = new();

    public void Deserialize(DataStream inStream, int inSubsetSize)
    {
        m_meshType = (MeshType)inStream.ReadInt32(inPad: true);
        m_maxInstances = inStream.ReadInt32(inPad: true);

        if (ProfilesLibrary.IsLoaded(ProfileVersion.Anthem))
        {
            m_unkEnum = inStream.ReadInt32(inPad: true); // some enum 0-5
        }

        int subsetCount = inStream.ReadInt32(inPad: true);
        m_subsets.EnsureCapacity(subsetCount);

        inStream.ReadRelocPtr(stream =>
        {
            for (int i = 0; i < subsetCount; i++)
            {
                MeshSubset subset = new();
                subset.Deserialize(stream, inSubsetSize);
                m_subsets.Add(subset);
            }
        });

        for (int i = 0; i < s_maxCategoryCount; i++)
        {
            int count = inStream.ReadInt32(inPad: true);
            inStream.ReadRelocPtr(stream =>
            {
                for (int j = 0; j < count; j++)
                {
                    m_subsets[stream.ReadByte()].Categories |= 1 << i;
                }
            });
        }

        m_flags = (MeshFlags)inStream.ReadInt32(inPad: true);

        // RenderFormat in > 2014.1 else 0, 1 for 32/16 bits
        int indexFormat = inStream.ReadInt32(inPad: true);
        if (ProfilesLibrary.FrostbiteVersion > "2014.1")
        {
            IndexFormat = (int)Enum.Parse(TypeLibrary.GetType("RenderFormat")!.Type, "RenderFormat_R32_UINT") == indexFormat ? 1 : 0;
        }
        else
        {
            IndexFormat = indexFormat;
        }

        IndexBufferSize = inStream.ReadUInt32(inPad: true);
        VertexBufferSize = inStream.ReadUInt32(inPad: true);

        if (s_hasAdjacencyInMesh)
        {
            uint size = inStream.ReadUInt32(inPad: true);
            if (size != 0)
            {
                // TODO:
            }
            // 0x150 * vertexCount if it exists
        }

        if (ProfilesLibrary.FrostbiteVersion >= "2021.1.1")
        {
            Debug.Assert(!inStream.ReadRelocPtr(null));
        }

        if (ProfilesLibrary.IsLoaded(ProfileVersion.DeadSpace, ProfileVersion.DragonAgeVeilguard) || ProfilesLibrary.FrostbiteVersion >= "2021.2.3")
        {
            inStream.ReadInt32();
        }

        ChunkId = inStream.ReadGuid(inPad: true);
        InlineVertexDataOffset = inStream.ReadInt32(inPad: true);
        Debug.Assert((m_flags.HasFlag(MeshFlags.StreamingEnable) && InlineVertexDataOffset == -1) ||
                     (!m_flags.HasFlag(MeshFlags.StreamingEnable) && InlineVertexDataOffset != -1));

        if (ProfilesLibrary.IsLoaded(ProfileVersion.DragonAgeVeilguard))
        {
            inStream.ReadInt32(); // uint.MaxValue
        }

        if (s_hasAdjacencyInMesh)
        {
            // TODO:
            inStream.ReadRelocPtr(null);
        }

        m_debugName = inStream.ReadString();
        Name = inStream.ReadString();
        ShortName = inStream.ReadString();
        m_nameHash = inStream.ReadUInt32(inPad: true);

        // some runtime ptr
        Debug.Assert(!inStream.ReadRelocPtr(null));

        if (m_meshType == MeshType.Skinned)
        {
            uint boneCount = inStream.ReadUInt32(inPad: true);
            // bone indices
            inStream.ReadRelocPtr(stream =>
            {
                for (int i = 0; i < boneCount; i++)
                {
                    BoneIndices.Add(stream.ReadUInt32());
                }
            });

            if (ProfilesLibrary.FrostbiteVersion < "2015.4")
            {
                // bone short names
                inStream.ReadRelocPtr(stream =>
                {
                    for (int i = 0; i < boneCount; i++)
                    {
                        BoneShortNames.Add(stream.ReadUInt32());
                    }
                });
            }
        }
        else if (m_meshType == MeshType.Composite)
        {
            if (ProfilesLibrary.FrostbiteVersion < "2015.4")
            {
                uint partCount = inStream.ReadUInt32(inPad: true);

                // part bounding boxes
                inStream.ReadRelocPtr(stream =>
                {
                    for (int i = 0; i < partCount; i++)
                    {
                        PartBoundingBoxes.Add(stream.ReadAabb());
                    }
                });

                // part transforms
                inStream.ReadRelocPtr(stream =>
                {
                    for (int i = 0; i < partCount; i++)
                    {
                        PartTransforms.Add(stream.ReadLinearTransform());
                    }
                });
            }
            // part indices
            inStream.ReadRelocPtr(stream =>
            {
                for (int s = 0; s < subsetCount; s++)
                {
                    List<int> sectionPartIndices = new();
                    for (int i = 0; i < 0x18; i++)
                    {
                        int b = stream.ReadByte();
                        for (int j = 0; j < 8; j++)
                        {
                            if ((b & 0x01) != 0)
                            {
                                sectionPartIndices.Add(i * 8 + j);
                            }
                            b >>= 1;
                        }
                    }
                    PartIndices.Add(sectionPartIndices);
                }
            });
        }

        inStream.Pad(16);
    }
}