using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject contentObject;
    public GameObject listButton;
    public GameObject scaleInput;

    public TextMeshProUGUI objectNameLabel;
    public GameObject itemContainer;

    private GameObject object3D;

    public delegate void ButtonDelegate(int objectNumber);

    private ObjectsPresenter objectsPresenter;
    private float buttonYOffset = 0f;

    
    void Start()
    {
        this.objectsPresenter = new ObjectsPresenter(this);
        
        scaleInput.GetComponent<TMP_InputField>().onValueChanged.AddListener((string inputString) => 
        {
            if (inputString == "") return;
            objectsPresenter.UpdateScale(float.Parse(inputString)); 
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

    public void SetObjectName(string objectName)
    {
        scaleInput.GetComponent<TMP_InputField>().interactable = true;
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
    
}
