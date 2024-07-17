using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPresenter
{
    private readonly Menu viewMenu;
    private ObjectsLibrary objectsLibrary;
    private int objectNumber;

    public enum MeshEnum { Cube, Sphere, Capsule };

    private Dictionary<MeshEnum, Mesh> meshesDictionary = new Dictionary<MeshEnum, Mesh> // move objects to enums
    {
        { MeshEnum.Cube, Resources.GetBuiltinResource<Mesh>("Cube.fbx") },
        { MeshEnum.Sphere, Resources.GetBuiltinResource<Mesh>("Sphere.fbx")},
        { MeshEnum.Capsule, Resources.GetBuiltinResource<Mesh>("Capsule.fbx")}
    };

    public ObjectsPresenter(Menu viewMenu)
    {
        this.viewMenu = viewMenu;
        this.objectsLibrary = new ObjectsLibrary();
        LoadObjects();
        LoadDropdowns();
    }

    public void LoadObjects()
    {
        List<ViewableObject> objectsList = objectsLibrary.GetViewableObjects();
        for (int i = 0; i < objectsList.Count; i++)
        {
            viewMenu.AddButton(objectsList[i].name, i, ShowObject);
        }
    }

    public void LoadDropdowns()
    {
        viewMenu.SetMeshOptions(new List<MeshEnum>(meshesDictionary.Keys));
    }

    public void ShowObject(int objectNumber)
    {
        this.objectNumber = objectNumber;
        ViewableObject viewableObject = objectsLibrary.GetViewableObjects()[objectNumber];
        viewMenu.SetObjectName(viewableObject.name);
        viewMenu.SetObjectMesh(viewableObject.mesh);
        viewMenu.SetObjectMaterial(viewableObject.material);
        viewMenu.SetObjectScale(viewableObject.scale);
        viewMenu.SetObjectColor(viewableObject.baseColor);
    }

    public void UpdateScale(float scale)
    {
        ViewableObject viewableObject = objectsLibrary.GetViewableObjects()[objectNumber];
        viewableObject.scale = scale;
        viewMenu.SetObjectScale(viewableObject.scale);
    }

    public void UpdateMesh(MeshEnum meshNumber)
    {
        ViewableObject viewableObject = objectsLibrary.GetViewableObjects()[objectNumber];
        viewableObject.mesh = meshesDictionary[meshNumber];
        viewMenu.SetObjectMesh(viewableObject.mesh);
        viewMenu.SetObjectMaterial(viewableObject.material);
        viewMenu.SetObjectScale(viewableObject.scale);
        viewMenu.SetObjectColor(viewableObject.baseColor);
    }

    public void UpdateAlpha(float alpha)
    {
        ViewableObject viewableObject = objectsLibrary.GetViewableObjects()[objectNumber];
        viewableObject.alpha = alpha;
        viewMenu.SetObjectAlpha(viewableObject.alpha);
    }
}
