using System.Numerics;
using FrostyMeshPlugin.Utils;

namespace FrostyMeshPlugin;

public class Mesh
{
    public string Name { get; }
    public Container Format { get; }

    public CoordinateSystem CoordinateSystem { get; }

    public UvSystem UvSystem { get; }

    internal List<Vector3> Positions { get; }
    internal List<Vector3> Normals { get; }
    internal List<Vector3> Tangents { get; }
    internal List<Vector3> Binormals { get; }
    internal List<List<Vector2>> Uvs { get; }
    internal List<List<Vector4>> Weights { get; }
    internal List<List<Vector4<uint>>> Joints { get; }
    internal List<uint> Indices { get; }
    internal PrimitiveType PrimitiveType { get; }
    private readonly Matrix4x4 m_transform;

    public Mesh(string inName, Container inContainer, CoordinateSystem inCoordinateSystem, UvSystem inUvSystem,
        PrimitiveType inPrimitiveType = PrimitiveType.Triangles, int inVertexCount = -1)
    {
        Name = inName;
        Format = inContainer;
        CoordinateSystem = inCoordinateSystem;
        UvSystem = inUvSystem;
        PrimitiveType = inPrimitiveType;
        Positions = inVertexCount == -1 ? new List<Vector3>() : new List<Vector3>(inVertexCount);
        Normals = inVertexCount == -1 ? new List<Vector3>() : new List<Vector3>(inVertexCount);
        Tangents = inVertexCount == -1 ? new List<Vector3>() : new List<Vector3>(inVertexCount);
        Binormals = inVertexCount == -1 ? new List<Vector3>() : new List<Vector3>(inVertexCount);
        Uvs = new List<List<Vector2>>();
        Weights = new List<List<Vector4>>();
        Joints = new List<List<Vector4<uint>>>();
        Indices = inVertexCount == -1 ? new List<uint>() : new List<uint>(inVertexCount * 3);
        m_transform = new Matrix4x4
        {
            [Format.CoordinateSystem.Up, CoordinateSystem.Up] = Format.CoordinateSystem.Up.Sign * CoordinateSystem.Up.Sign,
            [Format.CoordinateSystem.Forward, CoordinateSystem.Forward] = Format.CoordinateSystem.Forward.Sign * CoordinateSystem.Forward.Sign,
            [Format.CoordinateSystem.Right, CoordinateSystem.Right] = Format.CoordinateSystem.Right.Sign * CoordinateSystem.Right.Sign,
        };
    }

    public Vector2 ConvertToUvSystem(Vector2 inVec)
    {
        if (UvSystem == Format.UvSystem)
        {
            return inVec;
        }

        Vector2 retVal = default;
        retVal[Format.UvSystem.Up] = Format.UvSystem.Up.Sign * UvSystem.Up.Sign == -1
            ? 1 - inVec[UvSystem.Up]
            : inVec[UvSystem.Up];
        retVal[Format.UvSystem.Right] = Format.UvSystem.Right.Sign * UvSystem.Right.Sign == -1
            ? 1 - inVec[UvSystem.Right]
            : inVec[UvSystem.Right];
        return retVal;
    }

    public void AddVertexPosition(Vector3 inPosition)
    {
        Positions.Add(Vector3.TransformNormal(inPosition, m_transform));
    }

    public void AddVertexNormal(Vector3 inNormal)
    {
        Normals.Add(Vector3.TransformNormal(inNormal, m_transform));
    }

    public void AddVertexTangent(Vector3 inTangent)
    {
        Tangents.Add(Vector3.TransformNormal(inTangent, m_transform) * Format.UvSystem.Up.Sign * UvSystem.Up.Sign);
    }

    public void AddVertexBinormal(Vector3 inBinormal)
    {
        Binormals.Add(Vector3.TransformNormal(inBinormal, m_transform) * Format.UvSystem.Right.Sign * UvSystem.Right.Sign);
    }

    public void AddVertexUv(int inChannel, Vector2 inUv)
    {
        if (Uvs.Count <= inChannel)
        {
            Uvs.Add(new List<Vector2>());
        }
        Uvs[inChannel].Add(ConvertToUvSystem(inUv));
    }

    public void AddVertexWeights(int inIndex, Vector4 inWeights)
    {
        if (Weights.Count <= inIndex)
        {
            Weights.Add(new List<Vector4>());
        }
        Weights[inIndex].Add(inWeights);
    }

    public void AddVertexJoints(int inIndex, Vector4<uint> inJoints)
    {
        if (Joints.Count <= inIndex)
        {
            Joints.Add(new List<Vector4<uint>>());
        }
        Joints[inIndex].Add(inJoints);
    }

    public void AddIndex(uint index)
    {
        Indices.Add(index);
    }
}