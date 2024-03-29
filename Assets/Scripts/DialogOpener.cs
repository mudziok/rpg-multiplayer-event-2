using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class DialogOpener : MonoBehaviour, INotifyPropertyChanged
{
    private bool visited = false;

    public InteractiveElement activator;

    [SerializeField]
    private GameObject minigamePrefab;
    private MinigameBase minigame;

    [SerializeField]
    private string message;
    [SerializeField]
    private Sprite image;
    [SerializeField]
    private AudioClip sound;

    public event PropertyChangedEventHandler PropertyChanged;

    private void Awake()
    {
        activator.PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "IsActivated")
        {
            if (activator.IsActivated && !visited)
            { 
                minigame = MinigamesManager.Instance.StartMinigame(minigamePrefab);
                minigame.GetComponent<DialogMinigame>().SetMessage(message);
                minigame.GetComponent<DialogMinigame>().SetImage(image);
                minigame.GetComponent<DialogMinigame>().SetTalkingSound(sound);
                minigame.closedEvent += () => minigame = null;
                visited = true;
            }
            else
            {

            }
        }
    }

    private void RaisePropertyChanged(string propertyName)
    {
        var propChange = PropertyChanged;
        if (propChange == null) return;
        propChange(this, new PropertyChangedEventArgs(propertyName));
    }
}
