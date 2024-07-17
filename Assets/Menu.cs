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

    private Dictionary<ObjectsPresenter.MeshEnum, string> meshNameDictionary = new Dictionary<ObjectsPresenter.MeshEnum, string>()
    {
        { ObjectsPresenter.MeshEnum.Cube, "Cube" },
        { ObjectsPresenter.MeshEnum.Sphere, "Sphere" },
        { ObjectsPresenter.MeshEnum.Capsule, "Capsule" },
    };


    
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
            objectsPresenter.UpdateMesh((ObjectsPresenter.MeshEnum)newMeshNumber);
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
        buttonYOffset += 30f;
    }

    public void SetMeshOptions(List<ObjectsPresenter.MeshEnum> optionValues)
    {
        optionValues.ForEach((ObjectsPresenter.MeshEnum value) =>
        {
            meshDropdown.AddOptions(new List<string> { meshNameDictionary[value] });
        });
        
    }

    public void SetObjectName(string objectName)
    {
        scaleInput.interactable = true;
        meshDropdown.interactable = true;
        objectNameLabel.text = objectName;
    }

    public void SetObjectMesh(Mesh mesh)
    {
        foreach (Transform child in itemContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        object3D = new GameObject();
        object3D.transform.SetParent(itemContainer.transform);
        MeshFilter meshFilter = object3D.AddComponent<MeshFilter>();
        object3D.AddComponent<MeshRenderer>();
        meshFilter.sharedMesh = mesh;
    }

    public void SetSelectedMesh(ObjectsPresenter.MeshEnum meshNumber)
    {
        meshDropdown.SetValueWithoutNotify((int)meshNumber);
    }

    public void SetObjectMaterial(Material material)
    {
        MeshRenderer meshRenderer = object3D.GetComponent<MeshRenderer>();
        meshRenderer.material = material;
    }

    public void SetObjectScale(float scale)
    {
        object3D.transform.localScale = new Vector3(scale, scale, scale);
        scaleInput.GetComponent<TMP_InputField>().text = scale.ToString();
    }
    
    public void SetObjectColor(Color color)
    {
        object3D.GetComponent<MeshRenderer>().material.color = color;
        // colorDropdown.GetComponent<TMP_Dropdown>();
    }

    public void SetObjectAlpha(float alpha)
    {
        MeshRenderer meshRenderer = object3D.GetComponent<MeshRenderer>();
        Color newColor = meshRenderer.material.color;
        newColor.a = alpha;
        meshRenderer.material.color = newColor;
    }
}
