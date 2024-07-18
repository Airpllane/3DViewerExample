using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsLibrary
{
    public List<ViewableObject> objectList = new List<ViewableObject>();
    public ObjectsLibrary()
    {
        objectList.Add(new ViewableObject("Object 1",
            MeshReference.MeshType.Cube,
            MaterialReference.MaterialType.Standard,
            0.3f,
            ColorReference.ColorType.Green,
            1f));

        objectList.Add(new ViewableObject("Object 2",
            MeshReference.MeshType.Sphere,
            MaterialReference.MaterialType.Wireframe,
            0.6f,
            ColorReference.ColorType.Blue,
            1f));

        objectList.Add(new ViewableObject("Object 3",
            MeshReference.MeshType.Capsule,
            MaterialReference.MaterialType.Solid,
            0.1f,
            ColorReference.ColorType.Red,
            0.7f));
    }
}

public class ViewableObject
{
    public string name { get; set; }
    public MeshReference.MeshType mesh { get; set; }
    public MaterialReference.MaterialType material { get; set; }
    public float scale { get; set; }
    public ColorReference.ColorType baseColor { get; set; }
    public float alpha { get; set; }

    public ViewableObject(string name, MeshReference.MeshType mesh, MaterialReference.MaterialType material, float scale, ColorReference.ColorType baseColor, float alpha)
    {
        this.name = name;
        this.mesh = mesh;
        this.material = material;
        this.scale = scale;
        this.baseColor = baseColor;
        this.alpha = alpha;
    }
}