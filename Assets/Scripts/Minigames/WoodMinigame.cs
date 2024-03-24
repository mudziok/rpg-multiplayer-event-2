using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WoodMinigame : MinigameBase
{
    [Header("References")]
    [SerializeField]
    private RectTransform saw;

    [Header("Config")]
    [SerializeField]
    [Tooltip("Distance travelled by mouse required to cut one tree, normalized to screen width")]
    private float movementPerTree;
    [SerializeField]
    [Tooltip("Where mouse movement detection ends in X axis, normalized to screen width")]
    private float mouseBorder;

    [SerializeField]
    [ReadOnly]
    private float lastMouseX;
    [SerializeField]
    [ReadOnly]
    private float mouseDistance;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }

        UpdateSawPosition();
        UpdateMouseDistance();
        if (mouseDistance > movementPerTree)
        {
            PerformAction();
            mouseDistance -= movementPerTree;
        }
    }

    public override void Open()
    {
        base.Open();

        lastMouseX = GetNormalizedAndClampedMouseX();
        mouseDistance = 0;
    }

    private void UpdateSawPosition()
    {
        Vector2 sawPosition = saw.anchoredPosition;
        sawPosition.x = GetNormalizedAndClampedMouseX() * Screen.width;
        saw.anchoredPosition = sawPosition;
    }

    private void UpdateMouseDistance()
    {
        float mouseX = GetNormalizedAndClampedMouseX();
        mouseDistance += Mathf.Abs(lastMouseX - mouseX);
        lastMouseX = mouseX;
    }

    private float GetNormalizedAndClampedMouseX()
    {
        float normalized = Input.mousePosition.x / Screen.width;
        return Mathf.Clamp(normalized, 0.5f - mouseBorder, 0.5f + mouseBorder);
    }
}
