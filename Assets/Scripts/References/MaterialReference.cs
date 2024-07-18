using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialReference
{
    public enum MaterialType { Standard, Wireframe, Solid };

    public static Dictionary<MaterialType, Material> materialTypeToMaterial = new Dictionary<MaterialType, Material>
    {
        { MaterialType.Standard, new Material(Shader.Find("Standard")) },
        { MaterialType.Wireframe, new Material(Shader.Find("VR/SpatialMapping/Wireframe"))},
        { MaterialType.Solid, new Material(Shader.Find("Sprites/Default"))},
    };

    public static Dictionary<MaterialType, string> materialTypeToName = new Dictionary<MaterialType, string>()
    {
        { MaterialType.Standard, "Standard" },
        { MaterialType.Wireframe, "Wireframe" },
        { MaterialType.Solid, "Solid" },
    };

    public static List<MaterialType> colorableMaterials = new List<MaterialType>
    {
        MaterialType.Standard,
        MaterialType.Solid
    };

    public static List<MaterialType> alphaMaterials = new List<MaterialType>
    {
        MaterialType.Solid
    };
}
