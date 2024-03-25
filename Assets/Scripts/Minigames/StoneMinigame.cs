using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneMinigame : MinigameBase
{
    //Current state of the minigame. It changes in the following order:
    //(Ready->Hit)->FadeOut->FadeIn
    //      ^repeated N times
    private enum State
    {
        Ready,
        Hit,
        FadeOut,
        FadeIn
    }

    [Header("References")]
    [SerializeField]
    private RectTransform pickaxe;
    [SerializeField]
    private Image stone;

    [Header("Config")]
    [SerializeField]
    [Tooltip("Specifies sprites for levels of stone's destruction. Count of hits required to break the stone is equal to the size of this array")]
    private Sprite[] stonePhases;
    [SerializeField]
    private float pickaxeSpeed;
    [SerializeField]
    [Tooltip("Angle at which the pickaxe ends its movement")]
    private float pickaxeHitAngle;
    [SerializeField]
    private float stoneFadeDuration;

    [Header("Debug")]
    [SerializeField]
    [ReadOnly]
    private State state;

    private int phaseCounter;

    private void Awake()
    {
        closedEvent += OnClose;
    }

    private void OnClose()
    {
        StopAllCoroutines();
    }

    protected override void Update()
    {
        base.Update();

        if (state == State.Ready)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                state = State.Hit;
                StartCoroutine(RotationCoroutine(pickaxe, new Vector3(0, 0, pickaxeHitAngle), pickaxeSpeed, () =>
                StartCoroutine(RotationCoroutine(pickaxe, Vector3.zero, pickaxeSpeed, OnPickaxeHit))));
            }
        }
    }

    public override void Open()
    {
        base.Open();

        ResetMinigameState();
    }

    private void ResetMinigameState()
    {
        state = State.Ready;
        pickaxe.rotation = Quaternion.identity;
        phaseCounter = 0;
        stone.sprite = stonePhases[phaseCounter];
    }

    private void OnPickaxeHit()
    {
        state = State.Ready;
        phaseCounter++;
        if(stonePhases.Length == phaseCounter)
        {
            OnStoneBroken();
            phaseCounter = 0;
        }
        stone.sprite = stonePhases[phaseCounter];
    }

    private void OnStoneBroken()
    {
        pickaxe.gameObject.SetActive(false);
        PerformAction();
        state = State.FadeOut;
        StartCoroutine(ImageFadeCoroutine(stone, stoneFadeDuration, FadeDirection.FadeOut, OnStoneFadeOutEnd));
    }

    private void OnStoneFadeOutEnd()
    {
        phaseCounter = 0;
        stone.sprite = stonePhases[phaseCounter];
        state = State.FadeIn;
        StartCoroutine(ImageFadeCoroutine(stone, stoneFadeDuration, FadeDirection.FadeIn, OnStoneFadeInEnd));
    }

    private void OnStoneFadeInEnd()
    {
        state = State.Ready;
        pickaxe.gameObject.SetActive(true);
    }
}
