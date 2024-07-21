using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ViewableObject
{
    public string objectName;
    public MeshReference.MeshType Mesh;
    public MaterialReference.MaterialType Material;
    public float scale;
    public ColorReference.ColorType BaseColor;
    public float alpha;

    public ViewableObject(string objectName, MeshReference.MeshType mesh, MaterialReference.MaterialType material, float scale, ColorReference.ColorType baseColor, float alpha)
    {
        this.objectName = objectName;
        this.Mesh = mesh;
        this.Material = material;
        this.scale = scale;
        this.BaseColor = baseColor;
        this.alpha = alpha;
    }
}
