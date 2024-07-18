using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshReference
{
    public enum MeshType { Cube, Sphere, Capsule };

    public static Dictionary<MeshType, Mesh> meshTypeToMesh = new Dictionary<MeshType, Mesh>
    {
        { MeshType.Cube, Resources.GetBuiltinResource<Mesh>("Cube.fbx") },
        { MeshType.Sphere, Resources.GetBuiltinResource<Mesh>("Sphere.fbx")},
        { MeshType.Capsule, Resources.GetBuiltinResource<Mesh>("Capsule.fbx")}
    };

    public static Dictionary<MeshType, string> meshTypeToName = new Dictionary<MeshType, string>()
    {
        { MeshType.Cube, "Cube" },
        { MeshType.Sphere, "Sphere" },
        { MeshType.Capsule, "Capsule" },
    };
}
