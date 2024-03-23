using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// Klasa "Magazynu" dla generatora zasobów, tutaj zasoby s¹ przechowywane i transferowane do plecaka gracza
public class GeneratorDepot : MonoBehaviour
{
    public ResourceGenerator generator;
    public GridGameObjectCollection gridCollection;
    public InteractiveElement outTransferActivator;

    [SerializeField]
    private bool outTransferActivated = false;
    [SerializeField]
    private float outTransferTimer = 0f;
    [SerializeField]
    private float outTransferDelay = 2f;
    private void Awake()
    {
        gridCollection.prefab = generator.resource.prefab;
        generator.PropertyChanged += OnPropertyChanged;
        outTransferActivator.PropertyChanged += OnPropertyChanged;
    }

    private void Update()
    {
        if (outTransferActivated && generator.ResourceAmount > 0)
        {
            outTransferTimer += Time.deltaTime;
            if (outTransferTimer >= outTransferDelay)
            {
                outTransferTimer = 0f;
                if (outTransferActivator.PlayerGameObject.GetComponent<PlayerBackPack>().TransferIn(generator.resource, 1))
                {
                    generator.ResourceAmount -= 1;
                }
            }
        }
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "ResourceAmount")
        {
            //Jest przypadek ¿e np. ta courtyna uruchomi siê parê razy przy zmianie ResourceAmount wielokrotnym zanim zd¹¿y siê skoñczyæ - naprawiæ

            StartCoroutine(UpdateDepotStash());
        }
        if (e.PropertyName == "IsActivated")
        {
            outTransferActivated = outTransferActivator.IsActivated; 
        }
    }

    private IEnumerator UpdateDepotStash()
    {
        while (generator.ResourceAmount != gridCollection.gridObjects.Count)
        {
            if (generator.ResourceAmount > gridCollection.gridObjects.Count)
            {
                gridCollection.AddObjectToGrid();
            }
            else
            {
                gridCollection.RemoveObjectFromGrid();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

}
