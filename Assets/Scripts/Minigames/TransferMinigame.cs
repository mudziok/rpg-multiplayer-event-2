using UnityEngine;
using UnityEngine.UI;

public class TransferMinigame : MinigameBase
{
    [Header("References")]
    [SerializeField]
    private Image box;



    [Header("Config")]
    [SerializeField]
    private float boxFadeTime;
    [SerializeField]
    [Tooltip("Width of the drop area on the right in normalized screen width")]
    private float dropAreaWidth;

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
        //In drop area
        if(position.x > (1 - dropAreaWidth) * Screen.width)
        {
            boxDragAndDrop.enabled = false;
            StartCoroutine(ImageFadeCoroutine(box, boxFadeTime, FadeDirection.FadeOut, OnBoxFadeOutEnd));
        }
    }

    private void OnBoxFadeOutEnd()
    {
        box.rectTransform.anchoredPosition = boxStartPosition;
        PerformAction();
        StartCoroutine(ImageFadeCoroutine(box, boxFadeTime, FadeDirection.FadeIn, OnBoxFadeInEnd));
    }

    private void OnBoxFadeInEnd()
    {
        boxDragAndDrop.enabled = true;
    }

    private void OnClose()
    {
        StopAllCoroutines();
    }
}
