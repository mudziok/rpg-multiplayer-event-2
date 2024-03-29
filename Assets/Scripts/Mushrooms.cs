using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushrooms : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        // Find the camera by name
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        // Check if the camera was found
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found!");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("maybe jumpscare????");
            ChangeScreenColor();
        }
    }

    void ChangeScreenColor()
    {
        Color currentColor = mainCamera.backgroundColor;
        currentColor.r += 1.0f; 
        currentColor.r = Mathf.Clamp01(currentColor.r);
        mainCamera.backgroundColor = currentColor;
    }
}
