using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

//Komponent s�u��cy do generowania zasob�w, umieszczamy go na obiekcie kt�ry ma generowa� zasoby
public class ResourceGenerator : MonoBehaviour, INotifyPropertyChanged
{
    //Zmienne z Property Notify
    //Ilo�� zasobu w generatorze
    private int resourceAmount = 0;
    private bool isGenerating = false;

    public int ResourceAmount
    {
        get => resourceAmount;
        set
        {
            resourceAmount = value;
            RaisePropertyChanged("ResourceAmount");
        }
    }

    public bool IsGenerating { 
        get => isGenerating;
        set{
            isGenerating = value;
            RaisePropertyChanged("IsGenerating");
        } 
    }

    //Element aktywuj�cy generator
    public InteractiveElement activator;
    //Ilo�� generowanych jednostek zasobu na cykl
    public int generationAmount = 1;
    //Zas�b kt�ry generujemy
    public GameResource resource;

    [SerializeField]
    private GameObject minigamePrefab;
    private MinigameBase minigame;

    public event PropertyChangedEventHandler PropertyChanged;

    private void Awake()
    {
        activator.PropertyChanged += OnPropertyChanged;
        minigame = minigamePrefab.GetComponent<MinigameBase>();
        minigame.actionPerformedEvent += () => ResourceAmount += generationAmount;
        minigame.closedEvent += () => minigame = null;
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "IsActivated")
        {
            if (activator.IsActivated)
            {
                MinigamesManager.Instance.StartMinigame(minigamePrefab);
                isGenerating = true;
            }
            else
            {
                isGenerating = false;
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
