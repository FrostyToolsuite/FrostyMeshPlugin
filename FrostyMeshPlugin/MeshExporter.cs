using System.Diagnostics;
using System.Numerics;
using Frosty;
using Frosty.Sdk;
using Frosty.Sdk.Attributes;
using Frosty.Sdk.Ebx;
using Frosty.Sdk.IO;
using Frosty.Sdk.Managers;
using Frosty.Sdk.Managers.Entries;
using Frosty.Sdk.Utils;
using FrostyMeshPlugin.Gltf;
using FrostyMeshPlugin.Resources;
using FrostyMeshPlugin.Utils;
using Microsoft.Extensions.Logging;

namespace FrostyMeshPlugin;

[FrostyPlugin]
public class MeshExporter
{
    private static readonly CoordinateSystem s_frostbiteCoordinate = new() { Up = "Y", Forward = "Z", Right = "-X" };
    private static readonly UvSystem s_frostbiteUv = new() { Up = "-Y", Right = "X" };

    public static void Export(EbxAssetEntry inEbxEntry, string inPath)
    {
        EbxAsset meshAsset = AssetManager.GetEbxAsset(inEbxEntry);

        Export(meshAsset, inPath);
    }

    [ExportEbxFunction("MeshAsset")]
    public static bool Export(EbxAsset inAsset, string inPath)
    {
        ResAssetEntry? resEntry = AssetManager.GetResAssetEntry(inAsset.RootObject.GetProperty<ResourceRef>("MeshSetResource"));

        if (resEntry is null)
        {
            FrostyLogger.Logger?.LogError("No MeshSet referenced by ebx.");
            return false;
        }

        MeshSet meshSet = AssetManager.GetResAs<MeshSet>(resEntry);

        return Export(meshSet, inPath);
    }

    public static unsafe bool Export(MeshSet inMeshSet, string inPath)
    {
        Container container = new GltfContainer();

        Type? usageType = TypeLibrary.GetType("VertexElementUsage")?.Type;
        Type? formatType = TypeLibrary.GetType("VertexElementFormat")?.Type;

        if (usageType is null || formatType is null)
        {
            return false;
        }
        int texCoord0 = (int)Enum.Parse(usageType, "VertexElementUsage_TexCoord0");
        int boneIndices = (int)Enum.Parse(usageType, "VertexElementUsage_BoneIndices");
        int boneWeights = (int)Enum.Parse(usageType, "VertexElementUsage_BoneWeights");

        foreach (Resources.Mesh mesh in inMeshSet.Meshes)
        {
            Model model = new(mesh.ShortName);

            Block<byte> data;
            if (mesh.ChunkId == Guid.Empty)
            {
                data = new Block<byte>(inMeshSet.InlineData!.Ptr + mesh.InlineVertexDataOffset,
                    (int)(mesh.VertexBufferSize + mesh.IndexBufferSize));
                data.MarkMemoryAsFragile();
            }
            else
            {
                ChunkAssetEntry? chunkEntry = AssetManager.GetChunkAssetEntry(mesh.ChunkId);

                if (chunkEntry is null)
                {
                    FrostyLogger.Logger?.LogError("Chunk {} does not exist", mesh.ChunkId);
                    continue;
                }
                data = AssetManager.GetAsset(chunkEntry);
            }

            using BlockStream chunkStream = new(data);

            foreach (MeshSubset subset in mesh.Subsets)
            {
                if (!subset.IsOpaque)
                {
                    // not an opaque mesh ignore
                    continue;
                }

                // add PrimitiveType, should always be TriangleList anyways
                Mesh m = new(subset.Name, container, s_frostbiteCoordinate, s_frostbiteUv);

                chunkStream.Position = subset.VertexOffset;

                for (int s = 0; s < subset.GeometryDeclarationDesc.StreamCount; s++)
                {
                    GeometryDeclarationDesc.Stream stream = subset.GeometryDeclarationDesc.Streams[s];

                    if (stream.VertexStride == 0)
                    {
                        // no elements in this stream
                        continue;
                    }

                    for (int v = 0; v < subset.VertexCount; v++)
                    {
                        long startPos = chunkStream.Position;

                        // packed binormal
                        Vector3 normal = Vector3.Zero, tangent = Vector3.Zero;
                        float binormalSign = 0;

                        for (int e = 0; e < subset.GeometryDeclarationDesc.ElementCount; e++)
                        {
                            GeometryDeclarationDesc.Element element = subset.GeometryDeclarationDesc.Elements[e];

                            if (element.StreamIndex != s)
                            {
                                // not this stream
                                continue;
                            }

                            chunkStream.Position = startPos + element.Offset;

                            string usage = Enum.Parse(usageType, element.Usage.ToString()).ToString()!;
                            string format = Enum.Parse(formatType, element.Format.ToString()).ToString()!;

                            switch (usage)
                            {
                                case "VertexElementUsage_Pos":
                                {
                                    m.AddVertexPosition(chunkStream.ReadVertexAsVector3(format));
                                    break;
                                }
                                case "VertexElementUsage_Normal":
                                {
                                    m.AddVertexNormal(normal = chunkStream.ReadVertexAsVector3(format));
                                    break;
                                }
                                case "VertexElementUsage_Binormal":
                                {
                                    m.AddVertexBinormal(chunkStream.ReadVertexAsVector3(format));
                                    break;
                                }
                                case "VertexElementUsage_Tangent":
                                {
                                    m.AddVertexTangent(tangent = chunkStream.ReadVertexAsVector3(format));
                                    break;
                                }
                                case "VertexElementUsage_BinormalSign":
                                {
                                    binormalSign = chunkStream.ReadVertexAsSingle(format);
                                    break;
                                }
                                case "VertexElementUsage_Tangent_BinormalSign":
                                {
                                    Vector4 tangentBinormalSign = chunkStream.ReadVertexAsVector4(format);
                                    tangent = new Vector3(tangentBinormalSign.X, tangentBinormalSign.Y, tangentBinormalSign.Z);
                                    binormalSign = tangentBinormalSign.W;
                                    break;
                                }
                                case "VertexElementUsage_TangentSpace":
                                {
                                    Matrix4x4 tbn = chunkStream.ReadVertexAsMatrix(format);
                                    m.AddVertexTangent(new Vector3(tbn.M11, tbn.M12, tbn.M13));
                                    m.AddVertexBinormal(new Vector3(tbn.M21, tbn.M22, tbn.M23));
                                    m.AddVertexNormal(new Vector3(tbn.M31, tbn.M32, tbn.M33));
                                    break;
                                }
                                case "VertexElementUsage_TexCoord0":
                                case "VertexElementUsage_TexCoord1":
                                case "VertexElementUsage_TexCoord2":
                                case "VertexElementUsage_TexCoord3":
                                case "VertexElementUsage_TexCoord4":
                                case "VertexElementUsage_TexCoord5":
                                case "VertexElementUsage_TexCoord6":
                                case "VertexElementUsage_TexCoord7":
                                {
                                    Vector2 uv = chunkStream.ReadVertexAsVector2(format);
                                    m.AddVertexUv(element.Usage - texCoord0, uv);
                                    break;
                                }
                                case "VertexElementUsage_Color0":
                                case "VertexElementUsage_Color1":
                                {
                                    Vector4 color = chunkStream.ReadVertexAsVector4(format);
                                    //m.AddVertexColorLayer((int)element.Usage - (int)VertexElementUsage_Color0, color);
                                    break;
                                }
                                case "VertexElementUsage_BoneIndices":
                                case "VertexElementUsage_BoneIndices2":
                                {
                                    Vector4<uint> joints = chunkStream.ReadVertexAsVector4UInt(format);
                                    m.AddVertexJoints(element.Usage - boneIndices, joints);
                                    break;
                                }
                                case "VertexElementUsage_BoneWeights":
                                case "VertexElementUsage_BoneWeights2":
                                {
                                    Vector4 weights = chunkStream.ReadVertexAsVector4(format);
                                    m.AddVertexWeights(element.Usage - boneWeights, weights);
                                    break;
                                }
                                default:
                                    FrostyLogger.Logger?.LogDebug("Unimplemented VertexElementUsage: {}", usage);
                                    break;
                            }
                        }

                        if (binormalSign != 0)
                        {
                            m.AddVertexBinormal(Vector3.Cross(normal, tangent) * binormalSign);
                        }

                        chunkStream.Position = startPos + stream.VertexStride;
                    }
                }

                chunkStream.Position = mesh.VertexBufferSize + subset.StartIndex * mesh.PrimitiveSize;

                for (int i = 0; i < subset.PrimitiveCount; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        uint index;
                        if (mesh.IndexFormat == 1)
                        {
                            index = chunkStream.ReadUInt32();
                        }
                        else
                        {
                            index = chunkStream.ReadUInt16();
                        }

                        m.AddIndex(index);
                    }
                }

                model.Meshes.Add(m);
            }

            container.Models.Add(model);
            break;
        }

        if (container.Models.Count == 0)
        {
            return false;
        }

        container.Save(inPath);
        return true;
    }
}