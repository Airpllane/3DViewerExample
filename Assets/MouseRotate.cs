using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    public float speed = 1.0f;

    bool isMouseHeld = false;

    void Update()
    {
        if (isMouseHeld)
        {
            RotateObject(this.gameObject);
        }
    }

    private void OnMouseDown()
    {
        isMouseHeld = true;
    }

    private void OnMouseUp()
    {
        isMouseHeld = false;
    }

    void RotateObject(GameObject targetObject)
    {
        float dh = -speed * Input.GetAxis("Mouse X");
        float dv = speed * Input.GetAxis("Mouse Y");

        targetObject.transform.Rotate(new Vector3(dv, dh, 0), Space.World);
    }
}
