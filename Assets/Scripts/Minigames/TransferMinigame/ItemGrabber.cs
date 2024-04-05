using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
    private Vector3 mousePositionOffset;
    public float dragSensitivity = 1f;
    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.transform.rotation * (Input.mousePosition * dragSensitivity / Screen.width);
    }
    private void OnMouseDown()
    {
        mousePositionOffset = transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + mousePositionOffset;
    }
}
