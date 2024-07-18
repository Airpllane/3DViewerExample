using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManagerPresenter : MonoBehaviour
{
    public Menu viewMenu;
    public ObjectManager objectsLibrary;
    private int objectNumber;

    void Start()
    {
        LoadObjects();
        LoadDropdowns();

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

    public void LoadObjects()
    {
        List<ViewableObject> objectsList = objectsLibrary.objectList;
        for (int i = 0; i < objectsList.Count; i++)
        {
            viewMenu.AddButton(objectsList[i].name, i, ShowObject);
        }
    }

    public void LoadDropdowns()
    {
        viewMenu.SetMeshOptions(new List<MeshReference.MeshType>(MeshReference.meshTypeToMesh.Keys));
        viewMenu.SetMaterialOptions(new List<MaterialReference.MaterialType>(MaterialReference.materialTypeToMaterial.Keys));
        viewMenu.SetColorOptions(new List<ColorReference.ColorType>(ColorReference.colorTypeToColor.Keys));
    }

    public void ShowObject(int objectNumber)
    {
        this.objectNumber = objectNumber;
        ViewableObject viewableObject = objectsLibrary.objectList[objectNumber];
        GameObject renderedObject = viewableObject.RenderObject();
        viewMenu.SetObject(renderedObject);

        viewMenu.SetObjectName(viewableObject.name);
        viewMenu.SetScaleInput(viewableObject.scale);
        viewMenu.SetMeshDropdown(viewableObject.Mesh);
        viewMenu.SetMaterialDropdown(viewableObject.Material);
        viewMenu.SetColorDropdown(viewableObject.BaseColor);
        viewMenu.SetAlphaSlider(viewableObject.alpha);

        RelockFields(viewableObject.Material);
    }

    public void UpdateScale(float scale)
    {
        ViewableObject viewableObject = objectsLibrary.objectList[objectNumber];
        viewableObject.scale = scale;
        viewMenu.SetObject(viewableObject.RenderObject());

        viewMenu.SetScaleInput(viewableObject.scale);
    }

    public void UpdateMesh(MeshReference.MeshType meshNumber)
    {
        ViewableObject viewableObject = objectsLibrary.objectList[objectNumber];
        viewableObject.Mesh = meshNumber;
        viewMenu.SetObject(viewableObject.RenderObject());

        viewMenu.SetMeshDropdown(viewableObject.Mesh);
    }

    public void UpdateMaterial(MaterialReference.MaterialType materialNumber)
    {
        ViewableObject viewableObject = objectsLibrary.objectList[objectNumber];
        viewableObject.Material = materialNumber;
        viewMenu.SetObject(viewableObject.RenderObject());

        viewMenu.SetMaterialDropdown(viewableObject.Material);
        RelockFields(viewableObject.Material);
    }

    public void UpdateColor(ColorReference.ColorType colorNumber)
    {
        ViewableObject viewableObject = objectsLibrary.objectList[objectNumber];
        viewableObject.BaseColor = colorNumber;
        viewMenu.SetObject(viewableObject.RenderObject());

        viewMenu.SetColorDropdown(viewableObject.BaseColor);
    }

    public void UpdateAlpha(float alpha)
    {
        ViewableObject viewableObject = objectsLibrary.objectList[objectNumber];
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
