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
    }

    public void LoadObjects()
    {
        List<ViewableObject> objectsList = objectsLibrary.GetViewableObjects();
        for (int i = 0; i < objectsList.Count; i++)
        {
            viewMenu.AddButton(objectsList[i].name, i, ShowObject);
        }
    }

    public void ShowObject(int objectNumber)
    {
        this.objectNumber = objectNumber;
        ViewableObject viewableObject = objectsLibrary.GetViewableObjects()[objectNumber];
        viewMenu.SetObjectName(viewableObject.name);
        viewMenu.SetObjectMesh(viewableObject.mesh);
        viewMenu.SetObjectMaterial(viewableObject.material);
        viewMenu.SetObjectScale(viewableObject.scale);
    }

    public void UpdateScale(float scale)
    {
        ViewableObject viewableObject = objectsLibrary.GetViewableObjects()[objectNumber];
        viewableObject.scale = scale;
        viewMenu.SetObjectScale(viewableObject.scale);
    }
}
