using System;
using System.Numerics;
using Frosty.Sdk.IO;
using FrostyDataStreamUtils;

namespace FrostyMeshPlugin.Utils;

public static class DataStreamExtensions
{
    public static GeometryDeclarationDesc ReadGeometryDeclarationDesc(this DataStream stream)
    {
        stream.Pad(4);
        GeometryDeclarationDesc geomDeclDesc = new()
        {
            Elements = new GeometryDeclarationDesc.Element[GeometryDeclarationDesc.MaxElements],
            Streams = new GeometryDeclarationDesc.Stream[GeometryDeclarationDesc.MaxStreams]
        };

        for (int i = 0; i < GeometryDeclarationDesc.MaxElements; i++)
        {
            geomDeclDesc.Elements[i] = new GeometryDeclarationDesc.Element
            {
                Usage = stream.ReadByte(),
                Format = stream.ReadByte(),
                Offset = stream.ReadByte(),
                StreamIndex = stream.ReadByte()
            };
        }
        for (int i = 0; i < GeometryDeclarationDesc.MaxStreams; i++)
        {
            geomDeclDesc.Streams[i] = new GeometryDeclarationDesc.Stream
            {
                VertexStride = stream.ReadByte(),
                Classification = stream.ReadByte()
            };
        }

        geomDeclDesc.ElementCount = stream.ReadByte();
        geomDeclDesc.StreamCount = stream.ReadByte();
        stream.Pad(4);

        return geomDeclDesc;
    }

    public static Vector2 ReadVertexAsVector2(this DataStream stream, string inFormat)
    {
        switch (inFormat)
        {
            case "VertexElementFormat_Float2":
                return stream.ReadVector2();
            case "VertexElementFormat_Half2":
                return new Vector2((float)stream.ReadHalf(), (float)stream.ReadHalf());
            case "VertexElementFormat_Short2N":
                return new Vector2(stream.ReadInt16() / (float)short.MaxValue * 0.5f + 0.5f,
                    stream.ReadInt16() / (float)short.MaxValue * 0.5f + 0.5f);
            default:
                throw new Exception($"Can't convert {inFormat} to Float3");
        }
    }

    public static Vector3 ReadVertexAsVector3(this DataStream stream, string inFormat)
    {
        switch (inFormat)
        {
            case "VertexElementFormat_Float3":
                return stream.ReadVector3();
            case "VertexElementFormat_Float4":
                Vector4 vec4 = stream.ReadVector4();
                return new Vector3(vec4.X, vec4.Y, vec4.Z);
            case "VertexElementFormat_Half3":
                return new Vector3((float)stream.ReadHalf(), (float)stream.ReadHalf(), (float)stream.ReadHalf());
            case "VertexElementFormat_Half4":
                float x = (float)stream.ReadHalf();
                float y = (float)stream.ReadHalf();
                float z = (float)stream.ReadHalf();
                stream.ReadHalf();
                return new Vector3(x, y, z);
            default:
                throw new Exception($"Can't convert {inFormat} to Float3");
        }
    }

    public static Vector4 ReadVertexAsVector4(this DataStream stream, string inFormat)
    {
        switch (inFormat)
        {
            case "VertexElementFormat_Float3":
                return new Vector4(stream.ReadVector3(), 0);
            case "VertexElementFormat_Float4":
                return stream.ReadVector4();
            case "VertexElementFormat_Half3":
                return new Vector4((float)stream.ReadHalf(), (float)stream.ReadHalf(), (float)stream.ReadHalf(), 0);
            case "VertexElementFormat_Half4":
                float x = (float)stream.ReadHalf();
                float y = (float)stream.ReadHalf();
                float z = (float)stream.ReadHalf();
                float w = (float)stream.ReadHalf();
                return new Vector4(x, y, z, w);
            case "VertexElementFormat_UByte4N":
                return new Vector4(stream.ReadByte() / 255.0f, stream.ReadByte() / 255.0f,
                    stream.ReadByte() / 255.0f, stream.ReadByte() / 255.0f);
            case "VertexElementFormat_UShort4N":
                return new Vector4(stream.ReadUInt16() / 65535.0f, stream.ReadUInt16() / 65535.0f,
                    stream.ReadUInt16() / 65535.0f, stream.ReadUInt16() / 65535.0f);
            default:
                throw new Exception($"Can't convert {inFormat} to Float4");
        }
    }

    public static Vector4<uint> ReadVertexAsVector4UInt(this DataStream stream, string inFormat)
    {
        switch (inFormat)
        {
            case "VertexElementFormat_UByte4":
                return new Vector4<uint>(stream.ReadByte(), stream.ReadByte(), stream.ReadByte(), stream.ReadByte());
            case "VertexElementFormat_UShort4":
                return new Vector4<uint>(stream.ReadUInt16(), stream.ReadUInt16(), stream.ReadUInt16(), stream.ReadUInt16());
            default:
                throw new Exception($"Can't convert {inFormat} to UInt4");
        }
    }

    public static float ReadVertexAsSingle(this DataStream stream, string inFormat)
    {
        switch (inFormat)
        {
            case "VertexElementFormat_Float":
                return stream.ReadSingle();
            case "VertexElementFormat_Half":
                return (float)stream.ReadHalf();
            default:
                throw new Exception($"Can't convert {inFormat} to Float3");
        }
    }

    public static Matrix4x4 ReadVertexAsMatrix(this DataStream stream, string inFormat)
    {
        switch (inFormat)
        {
            case "VertexElementFormat_UInt":
            {
                // packed quaternion
                uint packed = stream.ReadUInt32();
                float invSqrt2 = 1.0f / MathF.Sqrt(2);
                float sqrt2 = MathF.Sqrt(2);

                // unpack flags
                uint greatestComponent = (packed >> 1) & 0x3;
                float sign = (packed & 1) != 0 ? -1.0f : 1.0f;

                // unpack quaternion
                float x = ((packed >> 22) & 1023) / 1023.0f * sqrt2 - invSqrt2;
                float y = ((packed >> 13) & 511) / 511.0f * sqrt2 - invSqrt2;
                float z = ((packed >> 3) & 1023) / 1023.0f * sqrt2 - invSqrt2;
                Vector3 unpacked = new(x, y, z);
                float w = MathF.Sqrt(1 - MathF.Min(Vector3.Dot(unpacked, unpacked), 1));

                Quaternion quat;
                switch (greatestComponent)
                {
                    case 0:
                        quat = new(w, x, y, z);
                        break;
                    case 1:
                        quat = new(x, w, y, z);
                        break;
                    case 2:
                        quat = new(x, y, w, z);
                        break;
                    default:
                        quat = new(x, y, z, w);
                        break;
                }

                // calculate normal and tangent from quaternion
                Vector3 normal = new(1 - 2 * (quat.Y * quat.Y + quat.Z * quat.Z),
                    quat.Y * 2 * quat.X + quat.Z * 2 * quat.W, quat.Z * 2 * quat.X - quat.Y * 2 * quat.W);
                Vector3 tangent = new(quat.Y * 2 * quat.X - quat.Z * 2 * quat.W,
                    1 - 2 * (quat.X * quat.X + quat.Z * quat.Z), quat.Y * 2 * quat.Z - quat.X * 2 * quat.W);

                // calculate binormal with sign
                Vector3 binormal = Vector3.Cross(normal, tangent);
                binormal *= sign;

                return new Matrix4x4(tangent.X, tangent.Y, tangent.Z, 1.0f, binormal.X, binormal.Y, binormal.Z, 1.0f, normal.X,
                    normal.Y, normal.Z, 1.0f, 0f, 0f, 0f, 1.0f);
            }
            case "VertexElementFormat_UByte4N":
            case "VertexElementFormat_UShort4N":
            {
                // axis angle
                Vector4 packed = stream.ReadVertexAsVector4(inFormat);

                uint flags = (uint)(packed.W * 255.0f + 0.25f);
                float invSqrt2 = 1.0f / MathF.Sqrt(2);

                Vector3 xyz = new(packed.X, packed.Y, packed.Z);

                Vector3 r0 = xyz * new Vector3(invSqrt2, invSqrt2, MathF.PI / 2) + new Vector3(-invSqrt2, -invSqrt2, 0);

                Vector3 r1 = new(flags & 16, flags & 32, flags & 192);
                Vector3 vec;
                vec.Y = r1.X * 0.0441942f + r0.X;
                vec.Z = r1.Y * 0.0220971f + r0.Y;
                Vector2 vec2 = new(vec.Y, vec.Z);
                vec.X = MathF.Sqrt(Math.Abs(1 - Vector2.Dot(vec2, vec2)));

                vec.X = (flags & 1) != 0 ? -vec.X : vec.X;
                Vector3 normal = (flags & 4) != 0 ? new Vector3(vec.X, vec.Y, vec.Z) : new Vector3(vec.Y, vec.X, vec.Z);
                normal = (flags & 2) != 0
                    ? new Vector3(normal.X, normal.Y, normal.Z)
                    : new Vector3(normal.Y, normal.Z, normal.X);
                Vector3 tangent = Vector3.Normalize((flags & 2) != 0
                    ? new Vector3(-normal.Y, normal.X, 0)
                    : new Vector3(normal.Z, -normal.X, 0));

                Vector3 binormal = Vector3.Cross(normal, tangent);
                (float Sin, float Cos) sinCos = MathF.SinCos(MathF.Abs((flags & 192) * 0.0245437f + r0.Z));
                tangent *= sinCos.Cos;
                tangent += binormal * sinCos.Sin;
                binormal = Vector3.Cross(normal, tangent);
                binormal = (flags & 8) != 0 ? -binormal : binormal;
                normal = Vector3.Normalize(normal);
                binormal = Vector3.Normalize(binormal);
                tangent = Vector3.Normalize(tangent);

                return new Matrix4x4(tangent.X, tangent.Y, tangent.Z, 1.0f, binormal.X, binormal.Y, binormal.Z, 1.0f, normal.X,
                    normal.Y, normal.Z, 1.0f, 0f, 0f, 0f, 1.0f);
            }
            default:
                throw new Exception($"Can't convert {inFormat} to Float3");
        }
    }
}