using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectManagerPresenter : MonoBehaviour
{
    public Transform contentView;
    public ObjectManager objectManager;
    public ObjectButtonPresenter objectButtonPresenter;
    public ObjectPresenter objectPresenter; 

    private int objectNumber;
    public delegate void ButtonDelegate(int objectNumber);

    void Start()
    {
        LoadObjects();
    }

    public void LoadObjects()
    {
        List<ViewableObject> objectsList = objectManager.objectList;
        for (int i = 0; i < objectsList.Count; i++)
        {
            AddButton(objectsList[i]);
        }
    }

    public void AddButton(ViewableObject model)
    {
        ObjectButtonPresenter buttonPresenter = Instantiate(objectButtonPresenter, contentView);
        buttonPresenter.InjectModel(model);

        buttonPresenter.ButtonClicked += ButtonPresenterClicked;
    }

    private void ButtonPresenterClicked(ViewableObject model)
    {
        objectPresenter.InjectModel(model);
    }
}