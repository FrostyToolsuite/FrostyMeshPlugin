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

        Export(meshSet, inPath);

        return true;
    }

    public static void Export(MeshSet inMeshSet, string inPath)
    {
        Container container = new GltfContainer();

        foreach (Resources.Mesh mesh in inMeshSet.Meshes)
        {
            Model model = new(mesh.ShortName);

            // TODO: inline
            if (mesh.ChunkId == Guid.Empty)
            {
                FrostyLogger.Logger?.LogWarning("Inline vertex data is not supported atm.");
                continue;
            }

            ChunkAssetEntry? chunkEntry = AssetManager.GetChunkAssetEntry(mesh.ChunkId);

            if (chunkEntry is null)
            {
                // TODO: error
                //continue;
            }

            using BlockStream chunkStream = new(new Block<byte>(File.ReadAllBytes($"/home/jona/Downloads/{mesh.ChunkId}.chunk")));

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

                            switch (element.Usage)
                            {
                                case VertexElementUsage.Pos:
                                {
                                    m.AddVertexPosition(chunkStream.ReadVertexAsVector3(element.Format));
                                    break;
                                }
                                case VertexElementUsage.Normal:
                                {
                                    m.AddVertexNormal(normal = chunkStream.ReadVertexAsVector3(element.Format));
                                    break;
                                }
                                case VertexElementUsage.Binormal:
                                {
                                    m.AddVertexBinormal(chunkStream.ReadVertexAsVector3(element.Format));
                                    break;
                                }
                                case VertexElementUsage.Tangent:
                                {
                                    m.AddVertexTangent(tangent = chunkStream.ReadVertexAsVector3(element.Format));
                                    break;
                                }
                                case VertexElementUsage.BinormalSign:
                                {
                                    // newer games have Tangent_BinormalSign
                                    if (ProfilesLibrary.FrostbiteVersion >= "2017")
                                    {
                                        Vector4 tangentBinormalSign = chunkStream.ReadVertexAsVector4(element.Format);
                                        tangent = new Vector3(tangentBinormalSign.X, tangentBinormalSign.Y, tangentBinormalSign.Z);
                                        binormalSign = tangentBinormalSign.W;
                                    }
                                    else
                                    {
                                        binormalSign = chunkStream.ReadVertexAsSingle(element.Format);
                                    }

                                    break;
                                }
                                case VertexElementUsage.TangentSpace:
                                {
                                    Matrix4x4 tbn = chunkStream.ReadVertexAsMatrix(element.Format);
                                    m.AddVertexTangent(new Vector3(tbn.M11, tbn.M12, tbn.M13));
                                    m.AddVertexBinormal(new Vector3(tbn.M21, tbn.M22, tbn.M23));
                                    m.AddVertexNormal(new Vector3(tbn.M31, tbn.M32, tbn.M33));
                                    break;
                                }
                                case VertexElementUsage.TexCoord0:
                                case VertexElementUsage.TexCoord1:
                                case VertexElementUsage.TexCoord2:
                                case VertexElementUsage.TexCoord3:
                                case VertexElementUsage.TexCoord4:
                                case VertexElementUsage.TexCoord5:
                                case VertexElementUsage.TexCoord6:
                                case VertexElementUsage.TexCoord7:
                                {
                                    Vector2 uv = chunkStream.ReadVertexAsVector2(element.Format);
                                    m.AddVertexUv((int)element.Usage - (int)VertexElementUsage.TexCoord0, uv);
                                    break;
                                }
                                case VertexElementUsage.Color0:
                                case VertexElementUsage.Color1:
                                {
                                    Debug.Assert(element.Format == VertexElementFormat.UByte4N, "Invalid Vertex Format");
                                    uint color = chunkStream.ReadUInt32(Endian.Big);
                                    //m.AddVertexColorLayer((int)element.Usage - (int)VertexElementUsage.Color0, color);
                                    break;
                                }
                                case VertexElementUsage.BoneIndices:
                                case VertexElementUsage.BoneIndices2:
                                {
                                    Vector4UI joints = chunkStream.ReadVertexAsVector4UI(element.Format);
                                    m.AddVertexJoints(element.Usage - VertexElementUsage.BoneIndices, joints);
                                    break;
                                }
                                case VertexElementUsage.BoneWeights:
                                case VertexElementUsage.BoneWeights2:
                                {
                                    Vector4 weights = chunkStream.ReadVertexAsVector4(element.Format);
                                    m.AddVertexWeights(element.Usage - VertexElementUsage.BoneWeights, weights);
                                    break;
                                }
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

        container.Save(inPath);
    }
}