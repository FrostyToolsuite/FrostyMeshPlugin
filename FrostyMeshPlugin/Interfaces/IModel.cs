using System.Collections.Generic;

namespace FrostyMeshPlugin.Interfaces;

public interface IModel
{
    public List<IMesh> Meshes { get; }
}