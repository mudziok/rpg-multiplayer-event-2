using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamesManager : Singleton<MinigamesManager>
{
    private GameObject currentMinigame;

    public TankPlayerController Player;

    public MinigameBase StartMinigame(GameObject minigameObject)
    {
        if (currentMinigame != null)
        {
            StopMinigame();
        }

        currentMinigame = minigameObject;
        if(currentMinigame.TryGetComponent(out MinigameBase minigame))
        {
            if (minigame.uiGame) minigame = Instantiate(currentMinigame, GameObject.Find("MinigamesCanvas").transform).GetComponent<MinigameBase>();
            Player.Pause();
            minigame.Open();
            minigame.closedEvent += StopMinigame;
            return minigame;
        }
        Debug.LogErrorFormat("No minigame script in minigame prefab {0}", minigameObject.name);
        currentMinigame = null;
        return null;
    }

    void StopMinigame()
    {
        currentMinigame = null;
        Player.Unpause();
    }
}
