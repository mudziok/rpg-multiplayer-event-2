using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

public class WoodMinigame : MinigameBase
{
    //Current state of the minigame. It should change in the following order:
    //Cutting->Falling->FadeOut->FadeIn
    private enum State
    {
        Cutting,
        Falling,
        FadeOut,
        FadeIn
    }

    [Header("References")]
    [SerializeField]
    private RectTransform saw;
    [SerializeField]
    private Image tree;

    [Header("Config")]
    [SerializeField]
    [Tooltip("Distance travelled by mouse required to cut one tree, normalized to screen width")]
    private float movementPerTree;
    [SerializeField]
    [Tooltip("Where mouse movement detection ends in X axis, normalized to screen width")]
    private float mouseBorder;
    [SerializeField]
    [Tooltip("In degees per second")]
    private float treeFallingSpeed;
    [SerializeField]
    [Tooltip("In seconds")]
    private float treeFadeDuration;

    [SerializeField]
    [ReadOnly]
    private float lastMouseX;
    [SerializeField]
    [ReadOnly]
    private float mouseDistance;
    [SerializeField]
    [ReadOnly]
    private State state;

    private void Awake()
    {
        gameObject.SetActive(false);
        closedEvent += OnClose;
    }

    private void OnClose()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }

        if (state == State.Cutting)
        {
            UpdateSawPosition();
            UpdateMouseDistance();
            if (mouseDistance > movementPerTree)
            {
                PerformAction();
                mouseDistance = 0;
                state = State.Falling;
                saw.gameObject.SetActive(false);
                //All of this could have been done using animations, but I've forgotten they exists
                StartCoroutine(RotationCoroutine(tree.rectTransform, new Vector3(0, 0, 90), treeFallingSpeed, OnTreeFallEnd));
            }
        }
    }

    public override void Open()
    {
        base.Open();

        lastMouseX = GetNormalizedAndClampedMouseX();
        ResetMinigameState();
    }

    private void ResetMinigameState()
    {
        mouseDistance = 0;
        state = State.Cutting;
        saw.gameObject.SetActive(true);
        tree.CrossFadeAlpha(1, 0, false);
        tree.rectTransform.rotation = Quaternion.identity;
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

    private void OnTreeFallEnd()
    {
        state = State.FadeOut;
        StartCoroutine(ImageFadeCoroutine(tree, treeFadeDuration, FadeDirection.FadeOut, OnTreeFadeOutEnd));
    }

    private void OnTreeFadeOutEnd()
    {
        state = State.FadeIn;
        tree.rectTransform.rotation = Quaternion.identity;
        StartCoroutine(ImageFadeCoroutine(tree, treeFadeDuration, FadeDirection.FadeIn, OnTreeFadeInEnd));
    }

    private void OnTreeFadeInEnd()
    {
        state = State.Cutting;
        saw.gameObject.SetActive(true);
    }
}
