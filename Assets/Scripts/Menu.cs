using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject contentObject;
    public GameObject listButton;
    public TMP_InputField scaleInput;
    public TMP_Dropdown meshDropdown;
    public TMP_Dropdown materialDropdown;
    public TMP_Dropdown colorDropdown;
    public Slider alphaSlider;

    public TextMeshProUGUI objectNameLabel;
    public GameObject itemContainer;
    [HideInInspector]
    public GameObject displayedObject;

    public delegate void ButtonDelegate(int objectNumber);

    private float buttonYOffset = 0f;

    void Start()
    {

    }

    public void AddButton(string text, int objectNumber, ButtonDelegate buttonDelegate)
    {
        GameObject button = Instantiate(listButton);
        button.transform.SetParent(contentObject.transform, false);
        button.GetComponentInChildren<TextMeshProUGUI>().text = text;
        button.GetComponent<Button>().onClick.AddListener(() => { buttonDelegate(objectNumber); });

        Vector3 buttonPosition = button.transform.localPosition;
        buttonPosition.y -= buttonYOffset;
        button.transform.localPosition = buttonPosition;
        buttonYOffset += button.GetComponent<RectTransform>().rect.height;
    }

    public void SetObject(GameObject renderedObject)
    {
        if (displayedObject) Destroy(displayedObject);
        displayedObject = renderedObject;
        displayedObject.transform.SetParent(itemContainer.transform);
        displayedObject.transform.localRotation = Quaternion.identity;
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
}
