using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectButtonPresenter : MonoBehaviour
{
    public Button button;
    public TMP_Text buttonText;
    public ViewableObject model = null;
    public event Action<ViewableObject> ButtonClicked;

    public void RaiseEvent()
    {
        ButtonClicked?.Invoke(model);
    }

    public void InjectModel(ViewableObject model)
    {
        this.model = model;
        buttonText.text = this.model.objectName;
        button.onClick.AddListener(RaiseEvent);
    }

    public void RemoveModel()
    {
        if (model == null) return;
        model = null;
        buttonText.text = "Null";
        button.onClick.RemoveListener(RaiseEvent);
    }
}
