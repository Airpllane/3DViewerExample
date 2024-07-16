using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject contentObject;
    public GameObject listButton;

    private float buttonYOffset = 0f;
    
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            AddButton();
        }
    }

    void AddButton()
    {
        GameObject button = Instantiate(listButton);
        button.transform.SetParent(contentObject.transform, false);

        Vector3 buttonPosition = button.transform.localPosition;
        Debug.Log(buttonPosition);
        buttonPosition.y -= buttonYOffset;
        button.transform.localPosition = buttonPosition;
        buttonYOffset += 30f;
        Debug.Log(buttonPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
