using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPresenter : MonoBehaviour
{
    public ViewableObject model;
    public Menu viewMenu;

    public void Awake()
    {
        viewMenu.scaleInput.onEndEdit.AddListener((string inputString) =>
        {
            float newScale;
            if (float.TryParse(inputString, out newScale))
            {
                if (newScale < 0.01) newScale = 0.01f;
                if (newScale > 1.5) newScale = 1.5f;
                UpdateScale(newScale);
            }
        });

        viewMenu.meshDropdown.onValueChanged.AddListener((int newMeshNumber) =>
        {
            UpdateMesh((MeshReference.MeshType)newMeshNumber);
        });

        viewMenu.materialDropdown.onValueChanged.AddListener((int newMaterialNumber) =>
        {
            UpdateMaterial((MaterialReference.MaterialType)newMaterialNumber);
        });

        viewMenu.colorDropdown.onValueChanged.AddListener((int newColorNumber) =>
        {
            UpdateColor((ColorReference.ColorType)newColorNumber);
        });

        viewMenu.alphaSlider.onValueChanged.AddListener((float newAlpha) =>
        {
            UpdateAlpha(newAlpha);
        });
    }

    public void DetachFromMenu()
    {
        viewMenu.scaleInput.onEndEdit.RemoveAllListeners();
        viewMenu.meshDropdown.onValueChanged.RemoveAllListeners();
        viewMenu.materialDropdown.onValueChanged.RemoveAllListeners();
        viewMenu.colorDropdown.onValueChanged.RemoveAllListeners();
        viewMenu.alphaSlider.onValueChanged.RemoveAllListeners();
    }

    public void InjectModel(ViewableObject model)
    {
        this.model = model;
        ShowObject();
    }

    public void ShowObject()
    {
        GameObject renderedObject = model.RenderObject();
        viewMenu.SetObject(renderedObject);

        viewMenu.SetObjectName(model.name);
        viewMenu.SetScaleInput(model.scale);
        viewMenu.SetMeshDropdown(model.Mesh);
        viewMenu.SetMaterialDropdown(model.Material);
        viewMenu.SetColorDropdown(model.BaseColor);
        viewMenu.SetAlphaSlider(model.alpha);

        RelockFields(model.Material);
    }

    public void UpdateScale(float scale)
    {
        ViewableObject viewableObject = model;
        viewableObject.scale = scale;
        viewMenu.SetObject(viewableObject.RenderObject());

        viewMenu.SetScaleInput(viewableObject.scale);
    }

    public void UpdateMesh(MeshReference.MeshType meshNumber)
    {
        ViewableObject viewableObject = model;
        viewableObject.Mesh = meshNumber;
        viewMenu.SetObject(viewableObject.RenderObject());

        viewMenu.SetMeshDropdown(viewableObject.Mesh);
    }

    public void UpdateMaterial(MaterialReference.MaterialType materialNumber)
    {
        ViewableObject viewableObject = model;
        viewableObject.Material = materialNumber;
        viewMenu.SetObject(viewableObject.RenderObject());

        viewMenu.SetMaterialDropdown(viewableObject.Material);
        RelockFields(viewableObject.Material);
    }

    public void UpdateColor(ColorReference.ColorType colorNumber)
    {
        ViewableObject viewableObject = model;
        viewableObject.BaseColor = colorNumber;
        viewMenu.SetObject(viewableObject.RenderObject());

        viewMenu.SetColorDropdown(viewableObject.BaseColor);
    }

    public void UpdateAlpha(float alpha)
    {
        ViewableObject viewableObject = model;
        viewableObject.alpha = alpha;
        viewMenu.SetObject(viewableObject.RenderObject());

        viewMenu.SetAlphaSlider(viewableObject.alpha);
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