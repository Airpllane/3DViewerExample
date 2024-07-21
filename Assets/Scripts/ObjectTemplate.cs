using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTemplate : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void SetObjectMesh(MeshReference.MeshType meshNumber)
    {
        meshFilter.sharedMesh = MeshReference.meshTypeToMesh[meshNumber];
    }

    public void SetObjectMaterial(MaterialReference.MaterialType materialNumber)
    {
        meshRenderer.material = MaterialReference.materialTypeToMaterial[materialNumber];
    }

    public void SetObjectScale(float scale)
    {
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void SetObjectColor(ColorReference.ColorType colorNumber)
    {
        gameObject.GetComponent<MeshRenderer>().material.color = ColorReference.colorTypeToColor[colorNumber];
    }

    public void SetObjectAlpha(float alpha)
    {
        Color newColor = meshRenderer.material.color;
        newColor.a = alpha;
        meshRenderer.material.color = newColor;
    }
}
