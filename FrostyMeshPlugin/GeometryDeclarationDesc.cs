using System.Text;
using Frosty.Sdk;

namespace FrostyMeshPlugin;

public struct GeometryDeclarationDesc
{
    public const int MaxElements = 16;
    public static readonly int MaxStreams = ProfilesLibrary.FrostbiteVersion <= "2014.4.11" ? 4 : ProfilesLibrary.FrostbiteVersion <= "2014.4.17" ? 6 : ProfilesLibrary.FrostbiteVersion < "2016.4" ? 8 : 16;

    public struct Element
    {
        public byte Usage;
        public byte Format;
        public byte Offset;
        public byte StreamIndex;
    }

    public struct Stream
    {
        public byte VertexStride;
        public byte Classification;
    }

    public Element[] Elements;
    public Stream[] Streams;
    public byte ElementCount;
    public byte StreamCount;
}