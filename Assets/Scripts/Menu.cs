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

    private GameObject object3D;

    public delegate void ButtonDelegate(int objectNumber);

    private ObjectsPresenter objectsPresenter;
    private float buttonYOffset = 0f;

    void Start()
    {
        this.objectsPresenter = new ObjectsPresenter(this);
        
        scaleInput.onEndEdit.AddListener((string inputString) => 
        {
            float newScale;
            if (float.TryParse(inputString, out newScale))
            {
                if (newScale < 0.01) newScale = 0.01f;
                if (newScale > 1.5) newScale = 1.5f;
                objectsPresenter.UpdateScale(newScale);
            }
        });

        meshDropdown.onValueChanged.AddListener((int newMeshNumber) =>
        {
            objectsPresenter.UpdateMesh((MeshReference.MeshType)newMeshNumber);
        });

        materialDropdown.onValueChanged.AddListener((int newMaterialNumber) =>
        {
            objectsPresenter.UpdateMaterial((MaterialReference.MaterialType)newMaterialNumber);
        });

        colorDropdown.onValueChanged.AddListener((int newColorNumber) =>
        {
            objectsPresenter.UpdateColor((ColorReference.ColorType)newColorNumber);
        });

        alphaSlider.onValueChanged.AddListener((float newAlpha) => 
        {
            objectsPresenter.UpdateAlpha(newAlpha);
        });
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

    public void SetObjectName(string objectName)
    {
        scaleInput.interactable = true;
        meshDropdown.interactable = true;
        materialDropdown.interactable = true;
        objectNameLabel.text = objectName;
    }

    public void SetObjectMesh(MeshReference.MeshType meshNumber)
    {
        Destroy(object3D);
        object3D = new GameObject();
        object3D.transform.SetParent(itemContainer.transform);
        MeshFilter meshFilter = object3D.AddComponent<MeshFilter>();
        object3D.AddComponent<MeshRenderer>();
        meshFilter.sharedMesh = MeshReference.meshTypeToMesh[meshNumber];
        meshDropdown.SetValueWithoutNotify((int)meshNumber);
    }

    public void SetObjectMaterial(MaterialReference.MaterialType materialNumber)
    {
        colorDropdown.interactable = false;
        alphaSlider.interactable = false;

        MeshRenderer meshRenderer = object3D.GetComponent<MeshRenderer>();
        meshRenderer.material = MaterialReference.materialTypeToMaterial[materialNumber];
        materialDropdown.SetValueWithoutNotify((int)materialNumber);

        if (MaterialReference.colorableMaterials.Contains(materialNumber))
        {
            colorDropdown.interactable = true;
        }
        if (MaterialReference.alphaMaterials.Contains(materialNumber))
        {
            alphaSlider.interactable = true;
        }
    }

    public void SetObjectScale(float scale)
    {
        object3D.transform.localScale = new Vector3(scale, scale, scale);
        scaleInput.GetComponent<TMP_InputField>().text = scale.ToString();
    }
    
    public void SetObjectColor(ColorReference.ColorType colorNumber)
    {
        object3D.GetComponent<MeshRenderer>().material.color = ColorReference.colorTypeToColor[colorNumber];
        colorDropdown.SetValueWithoutNotify((int)colorNumber);
    }

    public void SetObjectAlpha(float alpha)
    {
        MeshRenderer meshRenderer = object3D.GetComponent<MeshRenderer>();
        Color newColor = meshRenderer.material.color;
        newColor.a = alpha;
        meshRenderer.material.color = newColor;
        alphaSlider.value = alpha;
    }
}
