using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject contentObject;
    public TMP_InputField scaleInput;
    public TMP_Dropdown meshDropdown;
    public TMP_Dropdown materialDropdown;
    public TMP_Dropdown colorDropdown;
    public Slider alphaSlider;

    public event Action<string> OnScaleChanged;
    public event Action<MeshReference.MeshType> OnMeshChanged;
    public event Action<MaterialReference.MaterialType> OnMaterialChanged;
    public event Action<ColorReference.ColorType> OnColorChanged;
    public event Action<float> OnAlphaChanged;

    public TextMeshProUGUI objectNameLabel;
    public GameObject itemContainer;
    [HideInInspector]
    public GameObject displayedObject;

    void Start()
    {
        LoadDropdowns();

        scaleInput.onEndEdit.AddListener((string inputString) =>
        {
            OnScaleChanged.Invoke(inputString);
        });

        meshDropdown.onValueChanged.AddListener((int newMeshNumber) =>
        {
            OnMeshChanged.Invoke((MeshReference.MeshType)newMeshNumber);
        });

        materialDropdown.onValueChanged.AddListener((int newMaterialNumber) =>
        {
            OnMaterialChanged.Invoke((MaterialReference.MaterialType)newMaterialNumber);
        });

        colorDropdown.onValueChanged.AddListener((int newColorNumber) =>
        {
            OnColorChanged.Invoke((ColorReference.ColorType)newColorNumber);
        });

        alphaSlider.onValueChanged.AddListener((float newAlpha) =>
        {
            OnAlphaChanged.Invoke(newAlpha);
        });
    }

    public void SetObjectName(string objectName)
    {
        objectNameLabel.text = objectName;
    }

    public void SetScaleInput(float scale)
    {
        scaleInput.GetComponent<TMP_InputField>().text = scale.ToString();
    }

    public void SetMeshDropdown(MeshReference.MeshType meshType)
    {
        meshDropdown.SetValueWithoutNotify((int)meshType);
    }

    public void SetMaterialDropdown(MaterialReference.MaterialType materialType)
    {
        materialDropdown.SetValueWithoutNotify((int)materialType);
    }

    public void SetColorDropdown(ColorReference.ColorType colorType)
    {
        colorDropdown.SetValueWithoutNotify((int)colorType);
    }

    public void SetAlphaSlider(float alpha)
    {
        alphaSlider.value = alpha;
    }

    public void SetMeshOptions(List<MeshReference.MeshType> optionValues)
    {
        optionValues.ForEach((MeshReference.MeshType value) =>
        {
            meshDropdown.AddOptions(new List<string> { MeshReference.meshTypeToName[value] });
        });
    }

    public void SetMaterialOptions(List<MaterialReference.MaterialType> optionValues)
    {
        optionValues.ForEach((MaterialReference.MaterialType value) =>
        {
            materialDropdown.AddOptions(new List<string> { MaterialReference.materialTypeToName[value] });
        });
    }

    public void SetColorOptions(List<ColorReference.ColorType> optionValues)
    {
        optionValues.ForEach((ColorReference.ColorType value) =>
        {
            colorDropdown.AddOptions(new List<string> { ColorReference.colorTypeToName[value] });
        });
    }

    public void UnlockFields()
    {
        scaleInput.interactable = true;
        materialDropdown.interactable = true;
        meshDropdown.interactable = true;
        colorDropdown.interactable = true;
        alphaSlider.interactable = true;
    }

    public void LockColor()
    {
        colorDropdown.interactable = false;
    }

    public void LockAlpha()
    {
        alphaSlider.interactable = false;
    }
    public void LoadDropdowns()
    {
        SetMeshOptions(new List<MeshReference.MeshType>(MeshReference.meshTypeToMesh.Keys));
        SetMaterialOptions(new List<MaterialReference.MaterialType>(MaterialReference.materialTypeToMaterial.Keys));
        SetColorOptions(new List<ColorReference.ColorType>(ColorReference.colorTypeToColor.Keys));
    }
}
