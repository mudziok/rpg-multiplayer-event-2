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
    private GameObject dialogPrefab;
    private DialogBase dialog;

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
                dialog = DialogManager.Instance.InitDialog(dialogPrefab);
                dialog.GetComponent<DialogMinigame>().SetMessage(message);
                dialog.GetComponent<DialogMinigame>().SetImage(image);
                dialog.GetComponent<DialogMinigame>().SetTalkingSound(sound);
                dialog.closedEvent += () => dialog = null;
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
