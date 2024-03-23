using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

//Komponent s³u¿¹cy do generowania zasobów, umieszczamy go na obiekcie który ma generowaæ zasoby
public class ResourceGenerator : MonoBehaviour, INotifyPropertyChanged
{
    //Zmienne z Property Notify
    //Iloœæ zasobu w generatorze
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



    //Element aktywuj¹cy generator
    public InteractiveElement activator;
    //Czas generowania jednostki zasobu
    public float generationTime = 1.0f;
    //Iloœæ generowanych jednostek zasobu na cykl
    public int generationAmount = 1;
    //Zasób który generujemy
    public GameResource resource;
    //Timer generowania, tzn. ile czasu up³ynê³o od ostatniego wygenerowania u¿ywany do okreœlania czy ju¿ czas na wygenerowanie zasobu
    public float generationTimer = 0f;

    

    public event PropertyChangedEventHandler PropertyChanged;

    private void Awake()
    {
        activator.PropertyChanged += OnPropertyChanged;
    }

    private void Update()
    {
        if (IsGenerating)
        {
            OnProduction();
        }
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "IsActivated")
        {
            if (activator.IsActivated)
            {
                OnStartProduciton();
            }
            else
            {
                OnProductionStop();
            }
        }
    }

    private void RaisePropertyChanged(string propertyName)
    {
        var propChange = PropertyChanged;
        if (propChange == null) return;
        propChange(this, new PropertyChangedEventArgs(propertyName));
    }

    private void OnStartProduciton()
    {
        IsGenerating = true;
    }

    private void OnProduction()
    {
        generationTimer += Time.deltaTime;
        if (generationTimer >= generationTime)
        {
            ResourceAmount += generationAmount;
            generationTimer = 0f;
        }  
    }

    private void OnProductionStop()
    {
        IsGenerating = false;
    }
}
