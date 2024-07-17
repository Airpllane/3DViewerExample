using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsLibrary
{
    List<ViewableObject> objectList = new List<ViewableObject>();
    public ObjectsLibrary()
    {
        objectList.Add(new ViewableObject("Cube", 
            Resources.GetBuiltinResource<Mesh>("Cube.fbx"),
            new Material(Shader.Find("Sprites/Default")),
            1.2f));

        objectList.Add(new ViewableObject("Sphere", 
            Resources.GetBuiltinResource<Mesh>("Sphere.fbx"),
            new Material(Shader.Find("Standard")),
            0.5f));
    }

    public List<ViewableObject> GetViewableObjects()
    {
        return objectList;
    }
}

public class ViewableObject
{
    public string name { get; set; }
    public Mesh mesh { get; set; }
    public Material material { get; set; }
    public float scale { get; set; }
    private Color color;

    public ViewableObject(string name, Mesh mesh, Material material, float scale)
    {
        this.name = name;
        this.mesh = mesh;
        this.material = material;
        this.scale = scale;
    }
}