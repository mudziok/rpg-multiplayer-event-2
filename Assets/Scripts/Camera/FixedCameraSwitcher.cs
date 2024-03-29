using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCameraSwitcher : MonoBehaviour
{
    // Skrypt prze³¹czaj¹cy miêdzy kamerami, gdy postaæ wejdzie w przypisan¹ do nich strefê
    public Transform Player;
    // Kamera, któr¹ steruje dana strefa
    public CinemachineVirtualCamera activeCam;
    // Delegate i Event, które pozwalaj¹ nam na reakcjê na wejœcie u¿ytkownika do strefy nowej kamery
    public delegate void OnPrioritySetDelegate();
    public event OnPrioritySetDelegate OnPrioritySet;

    private void OnTriggerEnter(Collider other)
    {
        // Je¿eli gracz koliduje ze stref¹ kamery, podnieœ jej priorytet...
        if (other.CompareTag("Player"))
        {
            if (OnPrioritySet != null)
            {
                OnPrioritySet();
            }
            activeCam.Priority = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ...a gdy z niej wychodzi, obni¿ go
        if (other.CompareTag("Player"))
        {
            activeCam.Priority = 0;
        }
    }

    private void Update()
    {
        //Debug.Log(name + " " + activeCam.Priority);       DLACZEGO TA LINIJKA???!!!
    }
}

    


