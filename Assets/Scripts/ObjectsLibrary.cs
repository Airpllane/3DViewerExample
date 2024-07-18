using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsLibrary
{
    List<ViewableObject> objectList = new List<ViewableObject>();
    public ObjectsLibrary()
    {
        objectList.Add(new ViewableObject("Cube",
            MeshReference.MeshType.Cube,
            MaterialReference.MaterialType.Standard,
            1.2f,
            ColorReference.ColorType.Green,
            1f));

        objectList.Add(new ViewableObject("Sphere",
            MeshReference.MeshType.Sphere,
            MaterialReference.MaterialType.Wireframe,
            0.5f,
            ColorReference.ColorType.Blue,
            0.7f));
    }

    public List<ViewableObject> GetViewableObjects()
    {
        return objectList;
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