using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopActivator : MonoBehaviour
{
    [SerializeField]
    private GameObject minigamePrefab;
    private MinigameBase minigame;

    [SerializeField]
    private string message;
    [SerializeField]
    private Sprite image;
    [SerializeField]
    private AudioClip sound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            minigame = MinigamesManager.Instance.StartMinigame(minigamePrefab);
            minigame.GetComponent<DialogMinigame>().SetMessage(message);
            minigame.GetComponent<DialogMinigame>().SetImage(image);
            minigame.GetComponent<DialogMinigame>().SetTalkingSound(sound);
            minigame.GetComponent<ShopManager>().SetLabels();
            minigame.closedEvent += () => minigame = null;

        }

    }
}
