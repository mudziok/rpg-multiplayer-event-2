using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class PreasurePlate : InteractiveElement
{
    [Header("Zdarzenia reaguj¹ na gracza, czy wchodzi i wychodzi z zasiêgu collidera \n")]
    public UnityEvent onPlayerEnter;
    public UnityEvent onPlayerExit;

    private void Awake()
    {
        this.PropertyChanged += OnPropertyChanged;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            this.IsActivated = true;
            this.PlayerGameObject = other.gameObject;
        }
        
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            this.IsActivated = false;
            this.PlayerGameObject = null;
        }
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "IsActivated")
        {
            if(IsActivated) onPlayerEnter.Invoke();
            else onPlayerExit.Invoke();
        }
    }
}
