using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPresenter
{
    private readonly Menu viewMenu;
    private ObjectsLibrary objectsLibrary;
    private int objectNumber;

    public ObjectsPresenter(Menu viewMenu)
    {
        this.viewMenu = viewMenu;
        this.objectsLibrary = new ObjectsLibrary();
        LoadObjects();
        LoadDropdowns();
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
        viewMenu.SetObjectName(viewableObject.name);
        viewMenu.SetObjectMesh(viewableObject.mesh);
        viewMenu.SetObjectMaterial(viewableObject.material);
        viewMenu.SetObjectScale(viewableObject.scale);
        viewMenu.SetObjectColor(viewableObject.baseColor);
        viewMenu.SetObjectAlpha(viewableObject.alpha);
    }

    public void UpdateScale(float scale)
    {
        ViewableObject viewableObject = objectsLibrary.objectList[objectNumber];
        viewableObject.scale = scale;
        viewMenu.SetObjectScale(viewableObject.scale);
    }

    public void UpdateMesh(MeshReference.MeshType meshNumber)
    {
        ViewableObject viewableObject = objectsLibrary.objectList[objectNumber];
        viewableObject.mesh = meshNumber;
        viewMenu.SetObjectMesh(viewableObject.mesh);
        viewMenu.SetObjectMaterial(viewableObject.material);
        viewMenu.SetObjectScale(viewableObject.scale);
        viewMenu.SetObjectColor(viewableObject.baseColor);
        viewMenu.SetObjectAlpha(viewableObject.alpha);
    }

    public void UpdateMaterial(MaterialReference.MaterialType materialNumber)
    {
        ViewableObject viewableObject = objectsLibrary.objectList[objectNumber];
        viewableObject.material = materialNumber;
        viewMenu.SetObjectMaterial(viewableObject.material);
        viewMenu.SetObjectScale(viewableObject.scale);
        viewMenu.SetObjectColor(viewableObject.baseColor);
        viewMenu.SetObjectAlpha(viewableObject.alpha);
    }

    public void UpdateColor(ColorReference.ColorType colorNumber)
    {
        ViewableObject viewableObject = objectsLibrary.objectList[objectNumber];
        viewableObject.baseColor = colorNumber;
        viewMenu.SetObjectColor(viewableObject.baseColor);
        viewMenu.SetObjectAlpha(viewableObject.alpha);
    }

    public void UpdateAlpha(float alpha)
    {
        ViewableObject viewableObject = objectsLibrary.objectList[objectNumber];
        viewableObject.alpha = alpha;
        viewMenu.SetObjectAlpha(viewableObject.alpha);
    }
}
