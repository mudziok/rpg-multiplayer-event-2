using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamesManager : Singleton<MinigamesManager>
{
    [SerializeField]
    private SerializedDictionary<string, MinigameBase> minigames;
    public SerializedDictionary<string, MinigameBase> Minigames => minigames;

    private void Start()
    {
        foreach(MinigameBase minigame in Minigames.Values)
        {
            minigame.gameObject.SetActive(false);
        }
    }
}
