using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamesManager : Singleton<MinigamesManager>
{
    private GameObject currentMinigame;

    public TankPlayerController Player;

    public MinigameBase StartMinigame(GameObject minigamePrefab)
    {
        if (currentMinigame != null)
        {
            Destroy(currentMinigame);
        }
        currentMinigame = Instantiate(minigamePrefab, transform);
        if(currentMinigame.TryGetComponent(out MinigameBase minigame))
        {
            Player.Pause();
            minigame.SetPlayer(Player);
            minigame.closedEvent += () => currentMinigame = null;
            return minigame;
        }
        Debug.LogErrorFormat("No minigame script in minigame prefab {0}", minigamePrefab.name);
        currentMinigame = null;
        return null;
    }
}
