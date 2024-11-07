using System.Collections.Generic;

namespace FrostyMeshPlugin;

public class Model
{
    public string Name { get; }
    public List<Mesh> Meshes { get; } = new();

    public Model(string inName)
    {
        Name = inName;
    }
}