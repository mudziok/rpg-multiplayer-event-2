using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferMinigameV2 : MinigameBase
{
    [SerializeField] DropZone dropzone;
    [SerializeField] GeneratorDepot depot;

    private void Awake()
    {
        dropzone.onItemDrop += OnItemDropInDropZone;
    }

    public override void Open()
    {
        base.Open();
    }

    void OnItemDropInDropZone(GameObject go)
    {
        depot.TransferResource();
        PerformAction();
    }
}
