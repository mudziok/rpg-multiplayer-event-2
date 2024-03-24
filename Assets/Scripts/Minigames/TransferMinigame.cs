using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransferMinigame : MinigameBase
{
    private enum State
    {
        Transfer,
        FadeOut,
        FadeIn
    }

    [Header("References")]
    [SerializeField]
    private Image box;

    [Header("Config")]
    [SerializeField]
    private float boxFadeTime;
    [SerializeField]
    private float dropAreaWidth;

    [Header("Debug")]
    [SerializeField]
    [ReadOnly]
    private State state;

    private DragHandlerAndDropHandler boxDragAndDrop;
    private Vector2 boxStartPosition;

    private void Awake()
    {
        closedEvent += OnClose;
        if(!box.TryGetComponent(out boxDragAndDrop))
        {
            Debug.LogErrorFormat("No DragHandlerAndDropHandler in box in {0}", gameObject.name);
            return;
        }
        boxDragAndDrop.DropEvent += OnBoxDrop;
        boxStartPosition = box.rectTransform.anchoredPosition;
    }

    private void OnBoxDrop(Vector3 position)
    {
        if(position.x > (1 - dropAreaWidth) * Screen.width)
        {
            state = State.FadeOut;
            boxDragAndDrop.enabled = false;
            StartCoroutine(ImageFadeCoroutine(box, boxFadeTime, FadeDirection.FadeOut, OnBoxFadeOutEnd));
        }
    }

    private void OnBoxFadeOutEnd()
    {
        state = State.FadeIn;
        box.rectTransform.anchoredPosition = boxStartPosition;
        PerformAction();
        StartCoroutine(ImageFadeCoroutine(box, boxFadeTime, FadeDirection.FadeIn, OnBoxFadeInEnd));
    }

    private void OnBoxFadeInEnd()
    {
        state = State.Transfer;
        boxDragAndDrop.enabled = true;
    }

    private void OnClose()
    {
        StopAllCoroutines();
    }

    protected override void Update()
    {
        base.Update();
    }
}
