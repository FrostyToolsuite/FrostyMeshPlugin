using System.Collections.Generic;
using FrostyMeshPlugin.Utils;

namespace FrostyMeshPlugin;

public abstract class Container
{
    public abstract CoordinateSystem CoordinateSystem { get; }
    
    public abstract UvSystem UvSystem { get; }
    
    public List<Model> Models { get; } = new();

    public abstract void Load(string inPath);

    /// <summary>
    /// Saves this container in the underlying format
    /// </summary>
    /// <param name="inPath">The path where the file should be stored without the extension</param>
    public abstract void Save(string inPath);
}