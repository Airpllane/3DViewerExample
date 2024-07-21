using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPresenter : MonoBehaviour
{
    public ViewableObject model;
    public Menu viewMenu;
    public ObjectTemplate objectTemplate;
    public GameObject itemContainer;

    public GameObject renderedObject;
    public void Awake()
    {
        viewMenu.OnScaleChanged += UpdateScale;

        viewMenu.OnMeshChanged += UpdateMesh;

        viewMenu.OnMaterialChanged += UpdateMaterial;

        viewMenu.OnColorChanged += UpdateColor;

        viewMenu.OnAlphaChanged += UpdateAlpha;
    }

    public void DetachFromMenu()
    {
        viewMenu.OnScaleChanged -= UpdateScale;
        viewMenu.OnMeshChanged -= UpdateMesh;
        viewMenu.OnMaterialChanged -= UpdateMaterial;
        viewMenu.OnColorChanged -= UpdateColor;
        viewMenu.OnAlphaChanged -= UpdateAlpha;
    }

    public void InjectModel(ViewableObject model)
    {
        this.model = model;
        ShowObject();
    }

    public void ShowObject()
    {
        if (renderedObject) Destroy(renderedObject);
        renderedObject = Instantiate(objectTemplate.gameObject);

        ObjectTemplate renderedObjectTemplate = renderedObject.GetComponent<ObjectTemplate>();
        renderedObjectTemplate.SetObjectMesh(model.Mesh);
        renderedObjectTemplate.SetObjectMaterial(model.Material);
        renderedObjectTemplate.SetObjectScale(model.scale);
        renderedObjectTemplate.SetObjectColor(model.BaseColor);
        renderedObjectTemplate.SetObjectAlpha(model.alpha);

        
        renderedObject.transform.SetParent(itemContainer.transform);
        renderedObject.transform.localRotation = Quaternion.identity;
        RelockFields(model.Material);

        viewMenu.SetObjectName(model.objectName);
        viewMenu.SetScaleInput(model.scale);
        viewMenu.SetMeshDropdown(model.Mesh);
        viewMenu.SetMaterialDropdown(model.Material);
        viewMenu.SetColorDropdown(model.BaseColor);
        viewMenu.SetAlphaSlider(model.alpha);
    }

    public void UpdateScale(string scaleString)
    {
        float scale;
        if (float.TryParse(scaleString, out scale))
        {
            if (scale < 0.01) scale = 0.01f;
            if (scale > 1.5) scale = 1.5f;
        }
        model.scale = scale;
        ObjectTemplate renderedObjectTemplate = renderedObject.GetComponent<ObjectTemplate>();
        renderedObjectTemplate.SetObjectScale(scale);

        viewMenu.SetScaleInput(model.scale);
    }

    public void UpdateMesh(MeshReference.MeshType meshNumber)
    {
        model.Mesh = meshNumber;
        ObjectTemplate renderedObjectTemplate = renderedObject.GetComponent<ObjectTemplate>();
        renderedObjectTemplate.SetObjectMesh(meshNumber);

        viewMenu.SetMeshDropdown(model.Mesh);
    }

    public void UpdateMaterial(MaterialReference.MaterialType materialNumber)
    {
        model.Material = materialNumber;
        ObjectTemplate renderedObjectTemplate = renderedObject.GetComponent<ObjectTemplate>();
        renderedObjectTemplate.SetObjectMaterial(materialNumber);

        viewMenu.SetMaterialDropdown(model.Material);
        RelockFields(model.Material);

        UpdateColor(model.BaseColor);
        UpdateAlpha(model.alpha);
    }

    public void UpdateColor(ColorReference.ColorType colorNumber)
    {
        model.BaseColor = colorNumber;
        ObjectTemplate renderedObjectTemplate = renderedObject.GetComponent<ObjectTemplate>();
        renderedObjectTemplate.SetObjectColor(colorNumber);

        viewMenu.SetColorDropdown(model.BaseColor);
    }

    public void UpdateAlpha(float alpha)
    {
        model.alpha = alpha;
        ObjectTemplate renderedObjectTemplate = renderedObject.GetComponent<ObjectTemplate>();
        renderedObjectTemplate.SetObjectAlpha(alpha);

        viewMenu.SetAlphaSlider(model.alpha);
    }

    public void RelockFields(MaterialReference.MaterialType materialType)
    {
        viewMenu.UnlockFields();
        if (!MaterialReference.colorableMaterials.Contains(materialType))
        {
            viewMenu.LockColor();
        }
        if (!MaterialReference.alphaMaterials.Contains(materialType))
        {
            viewMenu.LockAlpha();
        }
    }
}