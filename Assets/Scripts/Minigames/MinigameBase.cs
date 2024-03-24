using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameBase : MonoBehaviour
{
    public event Action actionPerformedEvent;
    public event Action closedEvent;

    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    protected void Close()
    {
        closedEvent?.Invoke();
        gameObject.SetActive(false);
    }

    protected void PerformAction()
    {
        actionPerformedEvent?.Invoke();
    }
}
