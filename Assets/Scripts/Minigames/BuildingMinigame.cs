using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMinigame : MinigameBase
{
    //Current state of the minigame. It changes in the following order:
    //Ready->Hit->FadeOut->FadeIn
    private enum State
    {
        Ready,
        Hit,
        FadeOut,
        FadeIn
    }

    [Header("References")]
    [SerializeField]
    private Image hammer;

    [Header("Config")]
    [SerializeField]
    [Tooltip("Specifies sprites for levels of stone's destruction. Count of hits required to break the stone is equal to the size of this array")]
    private GameObject[] buildingPhases;
    [SerializeField]
    private float hammerSpeed;
    [SerializeField]
    [Tooltip("Angle at which the pickaxe ends its movement")]
    private float hammerHitAngle;
    [SerializeField]
    [Tooltip("Area where the hammer can spawn in normalized screen coordinates ([0,1])")]
    private Rect hammerPositionsRange;
    [SerializeField]
    private float hammerFadeDuration;

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
                StartCoroutine(RotationCoroutine(hammer.rectTransform, new Vector3(0, 0, hammerHitAngle), hammerSpeed, OnHammerHit));
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
        phaseCounter = 0;
        MoveHammer();
        SetBuildingPhase(phaseCounter);
    }

    private void SetBuildingPhase(int targetPhase)
    {
        for (int i = 0; i < buildingPhases.Length; i++)
        {
            GameObject phase = buildingPhases[i];
            phase.SetActive(i <= targetPhase);
        }
    }

    private void OnHammerHit()
    {
        phaseCounter++;
        Debug.Log("" + phaseCounter + " / " + buildingPhases.Length);
        if (buildingPhases.Length == phaseCounter)
        {
            Debug.Log("jest");
            PerformAction();
            Close();
        }
        else
        {
            SetBuildingPhase(phaseCounter);
            state = State.FadeOut;
            StartCoroutine(ImageFadeCoroutine(hammer, hammerFadeDuration, FadeDirection.FadeOut, OnHammerFadeOutEnd));
        }
    }

    private void OnHammerFadeOutEnd()
    {
        MoveHammer();
        state = State.FadeIn;
        StartCoroutine(ImageFadeCoroutine(hammer, hammerFadeDuration, FadeDirection.FadeIn, OnHammerFadeInEnd));
    }

    private void MoveHammer()
    {
        hammer.rectTransform.anchoredPosition = new Vector2(
            Random.Range(hammerPositionsRange.xMin, hammerPositionsRange.xMax) * Screen.width,
            Random.Range(hammerPositionsRange.yMin, hammerPositionsRange.yMax) * Screen.height);
        hammer.rectTransform.rotation = Quaternion.identity;
    }

    private void OnHammerFadeInEnd()
    {
        state = State.Ready;
    }
}
