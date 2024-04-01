using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBase : MonoBehaviour
{
    public event Action actionPerformedEvent;
    public event Action closedEvent;

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    void Close()
    {
        closedEvent?.Invoke();
        Destroy(gameObject); 
    }
}
