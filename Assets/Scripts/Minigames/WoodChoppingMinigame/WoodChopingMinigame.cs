using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodChopingMinigame : MinigameBase
{
    [SerializeField] TreeForChopping myTree;
    [SerializeField] AxeForChopping myAxe;
    private void Awake()
    {
        closedEvent += OnClose;
        myAxe.Init(myTree);
    }

    public override void Open()
    {
        base.Open();       
        myAxe.enabled = true;
        myTree.onTreeFall.AddListener(PerformAction);

    }
    protected override void Update()
    {
        base.Update();
    }
    private void OnClose()
    {
        myTree.onTreeFall.RemoveAllListeners();
        myTree.SetDefaults();
        myAxe.enabled = false;
    }
}
