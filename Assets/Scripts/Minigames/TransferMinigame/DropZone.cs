using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DropZone : MonoBehaviour
{
    public Action<GameObject> onItemDrop;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ItemGrabber>() != null)
        {
            onItemDrop.Invoke(other.gameObject);
        }
    }
}
