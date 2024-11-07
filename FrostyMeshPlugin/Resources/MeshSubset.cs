using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Frosty.Sdk;
using Frosty.Sdk.IO;
using Frosty.Sdk.Profiles;
using FrostyDataStreamUtils;
using FrostyMeshPlugin.Utils;

namespace FrostyMeshPlugin.Resources;

public class MeshSubset
{
    public static bool IsWeirdDiceFbFormat = ProfilesLibrary.IsLoaded(ProfileVersion.MirrorsEdgeCatalyst,
        ProfileVersion.Battlefield1, ProfileVersion.StarWarsBattlefrontII, ProfileVersion.Battlefield5,
        ProfileVersion.StarWarsSquadrons);

    public string Name { get; set; } = string.Empty;

    public int MaterialIndex { get; set; }
    public int LightMapUvIndex { get; set; }
    public float[] TexCoordRatios { get; } = new float[6];

    public uint PrimitiveCount { get; set; }
    public uint StartIndex { get; set; }
    public uint VertexOffset { get; set; }
    public uint VertexCount { get; set; }

    public byte VertexStride { get; set; }
    public byte PrimitiveType { get; set; }
    public byte BonesPerVertex { get; set; }
    public int Flags { get; set; }
    public ushort BoneCount { get; set; }
    public List<ushort> BoneIndices { get; set; } = new();
    public GeometryDeclarationDesc GeometryDeclarationDesc { get; private set; }
    public GeometryDeclarationDesc SecondGeometryDeclarationDesc { get; private set; }
    public ulong Hash { get; private set; }
    public uint UnkFlags { get; private set; }
    public BoundingBox BoundingBox { get; set; }

    public int Categories { get; set; }
    public bool IsOpaque => (Categories & 1) != 0;

    public void Deserialize(DataStream inStream, uint inSubsetSize)
    {
        long start = inStream.Position;

        // probably geometrydecldesc runtime pointer
        Debug.Assert(!inStream.ReadRelocPtr(null));

        if (IsWeirdDiceFbFormat)
        {
            // they store 2 geometrydecldesc runtime pointer so 2 runtime pointers
            Debug.Assert(!inStream.ReadRelocPtr(null));
        }

        Name = inStream.ReadString();

        if (ProfilesLibrary.FrostbiteVersion >= "2020.0")
        {
            inStream.ReadRelocPtr(stream =>
            {
                for (int i = 0; i < BoneCount; i++)
                {
                    BoneIndices.Add(stream.ReadUInt16(inPad: true));
                }
            }, stream =>
            {
                BoneCount = stream.ReadUInt16(inPad: true);
            });

            ushort flags = inStream.ReadUInt16(inPad: true);
            BonesPerVertex = (byte)(flags & 0xF);
            Flags = flags >> 4; // 1 << 7 when ZOnly/Shadow subset

            MaterialIndex = inStream.ReadUInt16(inPad: true);

            VertexStride = inStream.ReadByte();
            PrimitiveType = inStream.ReadByte();

            PrimitiveCount = inStream.ReadUInt32(inPad: true);
            StartIndex = inStream.ReadUInt32(inPad: true);
            VertexOffset = inStream.ReadUInt32(inPad: true);
            VertexCount = inStream.ReadUInt32(inPad: true);

            uint unk2 = inStream.ReadUInt32(); // maybe EnlightenType enum

            if (ProfilesLibrary.FrostbiteVersion >= "2021.1.1")
            {
                if (ProfilesLibrary.IsLoaded(ProfileVersion.DeadSpace, ProfileVersion.DragonAgeVeilguard))
                {
                    Debug.Assert(!inStream.ReadRelocPtr(null));
                }
                // probably some runtime ptr
                Debug.Assert(!inStream.ReadRelocPtr(null));
            }

            // texcoord ratios
            for (int i = 0; i < 6; i++)
            {
                TexCoordRatios[i] = inStream.ReadSingle(inPad: true);
            }
        }
        else
        {
            MaterialIndex = inStream.ReadInt32(inPad: true);

            if (ProfilesLibrary.FrostbiteVersion > "2014.1")
            {
                LightMapUvIndex = inStream.ReadInt32(inPad: true);
            }

            if (ProfilesLibrary.FrostbiteVersion >= "2019.0")
            {
                // texcoord ratios
                for (int i = 0; i < 6; i++)
                {
                    TexCoordRatios[i] = inStream.ReadSingle(inPad: true);
                }
            }

            PrimitiveCount = inStream.ReadUInt32(inPad: true);
            StartIndex = inStream.ReadUInt32(inPad: true);
            VertexOffset = inStream.ReadUInt32(inPad: true);
            VertexCount = inStream.ReadUInt32(inPad: true);

            // fb 2019 stores boundingBox not at the end
            if (ProfilesLibrary.FrostbiteVersion >= "2019.0")
            {
                BoundingBox = inStream.ReadAabb(inPad: true);
            }

            VertexStride = inStream.ReadByte();
            PrimitiveType = inStream.ReadByte();

            if (ProfilesLibrary.FrostbiteVersion >= "2019.0")
            {
                // 2 booleans
                inStream.ReadBoolean();
                inStream.ReadBoolean();
            }

            if (IsWeirdDiceFbFormat)
            {
                // probably related to the second geometrydecldesc
                uint unk1 = inStream.ReadUInt32(inPad: true); // always 24 or 0 in mec
                uint unk2 = inStream.ReadUInt32(inPad: true); // 0, 0x17C58, 0x17AA8, 0x17A00, 0x17D90, 0x1CB18 in mec
                uint unk3 = inStream.ReadUInt32(inPad: true); // always 13 or 0 in mec
            }

            BonesPerVertex = inStream.ReadByte();
            if (ProfilesLibrary.FrostbiteVersion > "2014.4.18")
            {
                BoneCount = inStream.ReadUInt16(inPad: true);
            }
            else
            {
                BoneCount = inStream.ReadByte();
            }

            // boneIndices
            inStream.ReadRelocPtr(stream =>
            {
                for (int i = 0; i < BoneCount; i++)
                {
                    BoneIndices.Add(stream.ReadUInt16(inPad: true));
                }
            });

            // Fifa18/SWBF2/NFS Payback/Anthem
            if (ProfilesLibrary.IsLoaded(ProfileVersion.StarWarsBattlefrontII, ProfileVersion.NeedForSpeedPayback,
                ProfileVersion.Fifa18, ProfileVersion.Madden19,
                ProfileVersion.Fifa19, ProfileVersion.Anthem,
                ProfileVersion.Madden20, ProfileVersion.Fifa20,
                ProfileVersion.PlantsVsZombiesBattleforNeighborville, ProfileVersion.NeedForSpeedHeat,
                ProfileVersion.StarWarsSquadrons))
            {
                // runtime ptr?
                Debug.Assert(!inStream.ReadRelocPtr(null));
            }

            // Fifa 17/18
            if (ProfilesLibrary.IsLoaded(ProfileVersion.Fifa17, ProfileVersion.Fifa18,
                ProfileVersion.Madden19))
            {
                inStream.ReadRelocPtr(stream =>
                {
                    // TODO:
                });

                inStream.ReadRelocPtr(stream =>
                {
                    // TODO:
                });

                inStream.ReadRelocPtr(stream =>
                {
                    // TODO:
                });
            }

            if (ProfilesLibrary.FrostbiteVersion >= "2019.0")
            {
                // some hash
                Hash = inStream.ReadUInt64(inPad: true);
            }
        }

        GeometryDeclarationDesc = inStream.ReadGeometryDeclarationDesc();

        if (IsWeirdDiceFbFormat)
        {
            SecondGeometryDeclarationDesc = inStream.ReadGeometryDeclarationDesc();
        }

        if (ProfilesLibrary.FrostbiteVersion < "2019.0")
        {
            // texcoord ratios
            for (int i = 0; i < 6; i++)
            {
                TexCoordRatios[i] = inStream.ReadSingle(inPad: true);
            }

            // until fb 2018 the bounding box is probably generated it at runtime
            BoundingBox = inStream.ReadAabb(inPad: true);

            if (ProfilesLibrary.FrostbiteVersion > "2014.1")
            {
                // there seems to be some garbage in the bounding box of dai
                Debug.Assert(!(ProfilesLibrary.FrostbiteVersion < "2018" && BoundingBox.Max - BoundingBox.Min != Vector4.Zero));
            }
        }

        if (ProfilesLibrary.FrostbiteVersion >= "2020.0")
        {
            if (ProfilesLibrary.IsLoaded(ProfileVersion.DragonAgeVeilguard))
            {
                inStream.ReadUInt64();
            }
            Hash = inStream.ReadUInt64(); // some hash probably same one as fb 2019 hash before geomdecldesc
            UnkFlags = inStream.ReadUInt32(); // 0x1505 for ZOnly + Shadow subsets
            uint unkHash3 = inStream.ReadUInt32(); // some other hash can be 0
            //Debug.Assert(!(unkHash3 == 0 && unkHash2 != 0));

            if (ProfilesLibrary.IsLoaded(ProfileVersion.DeadSpace))
            {
                inStream.ReadUInt64();
                inStream.ReadUInt64();
            }

            if (ProfilesLibrary.IsLoaded(ProfileVersion.DragonAgeVeilguard))
            {
                inStream.ReadUInt64();
            }

            BoundingBox = inStream.ReadAabb(inPad: true);
        }

        inStream.Pad(16);

        Debug.Assert(inStream.Position - start == inSubsetSize, "Didnt read MeshSubset correctly");
    }
}