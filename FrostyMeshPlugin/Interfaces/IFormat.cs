using System.Collections.Generic;
using FrostyMeshPlugin.Utils;

namespace FrostyMeshPlugin.Interfaces;

public interface IFormat
{
    public CoordinateSystem CoordinateSystem { get; }
    
    public UvSystem UvSystem { get; }
    
    public List<IModel> Models { get; }
}