using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class ResourceGeneratorBasicUI : MonoBehaviour
{
    public ResourceGenerator resourceGenerator;
    public TextMeshProUGUI resourceAmountText;
    public TextMeshProUGUI generationStateText;

    private void Awake()
    {
        // Pod³¹czamy siê do zdarzenia zmiany iloœci zasobów w strukturze - tak by UI by³o automatycznie aktualizowane przy zmienia stanu zasobów
        resourceGenerator.PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "ResourceAmount")
        {
            resourceAmountText.text = resourceGenerator.ResourceAmount.ToString();
        }
        else if (e.PropertyName == "IsGenerating")
        {
            if (resourceGenerator.IsGenerating)
            {
                generationStateText.text = "Working";
            }
            else
            {
                generationStateText.text = "Idle";
            }
        }
    }


}
