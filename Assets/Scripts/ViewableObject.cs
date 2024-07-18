using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewableObject : MonoBehaviour
{
    public string objectName;
    public MeshReference.MeshType Mesh;
    public MaterialReference.MaterialType Material;
    public float scale;
    public ColorReference.ColorType BaseColor;
    public float alpha;
    public GameObject RenderObject()
    {
        GameObject renderedObject = new GameObject();

        SetObjectMesh(renderedObject, Mesh);
        SetObjectMaterial(renderedObject, Material);
        SetObjectScale(renderedObject, scale);
        SetObjectColor(renderedObject, BaseColor);
        SetObjectAlpha(renderedObject, alpha);

        return renderedObject;
    }

    public void SetObjectMesh(GameObject renderedObject, MeshReference.MeshType meshNumber)
    {
        MeshFilter meshFilter = renderedObject.AddComponent<MeshFilter>();
        renderedObject.AddComponent<MeshRenderer>();
        meshFilter.sharedMesh = MeshReference.meshTypeToMesh[meshNumber];
    }

    public void SetObjectMaterial(GameObject renderedObject, MaterialReference.MaterialType materialNumber)
    {
        MeshRenderer meshRenderer = renderedObject.GetComponent<MeshRenderer>();
        meshRenderer.material = MaterialReference.materialTypeToMaterial[materialNumber];
    }

    public void SetObjectScale(GameObject renderedObject, float scale)
    {
        renderedObject.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void SetObjectColor(GameObject renderedObject, ColorReference.ColorType colorNumber)
    {
        renderedObject.GetComponent<MeshRenderer>().material.color = ColorReference.colorTypeToColor[colorNumber];
    }

    public void SetObjectAlpha(GameObject renderedObject, float alpha)
    {
        MeshRenderer meshRenderer = renderedObject.GetComponent<MeshRenderer>();
        Color newColor = meshRenderer.material.color;
        newColor.a = alpha;
        meshRenderer.material.color = newColor;
    }
}
