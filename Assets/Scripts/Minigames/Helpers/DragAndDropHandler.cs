using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandlerAndDropHandler : MonoBehaviour, IPointerDownHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private float pickupSizeMultipler;

    private Vector3 startScale;

    public event Action<Vector3> DropEvent;

    private void Start()
    {
        startScale = transform.localScale;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localScale = startScale;
        DropEvent?.Invoke(transform.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = startScale * pickupSizeMultipler;
    }
}
