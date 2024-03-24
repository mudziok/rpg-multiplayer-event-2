using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerBackPack : MonoBehaviour, INotifyPropertyChanged
{
    // Zmienne z notifyPropertyChanged
    private int resourceAmount;
    public int ResourceAmount
    {
        get { return resourceAmount; }
        set
        {
            resourceAmount = value;
            RaisePropertyChanged("ResourceAmount");
        }
    }

    // Zasób w plecaku
    public GameResource resource;
    public int maxResourceAmount = 10;
    // Kolekcja zasobów w plecaku, grid do po³o¿enia zasobów w widoku gry
    public GridGameObjectCollection gridCollection;


    public event PropertyChangedEventHandler PropertyChanged;

    private void RaisePropertyChanged(string propertyName)
    {
        var propChange = PropertyChanged;
        if (propChange == null) return;
        propChange(this, new PropertyChangedEventArgs(propertyName));
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "ResourceAmount")
        {
            //Jest przypadek ¿e np. ta courtyna uruchomi siê parê razy przy zmianie ResourceAmount wielokrotnym zanim zd¹¿y siê skoñczyæ - naprawiæ
            StartCoroutine(UpdateBackPackGridCollection());
        }
    }

    private void Awake()
    {
        this.PropertyChanged += OnPropertyChanged;
    }

    public bool IsInTransferPosibble(GameResource resourceType, int amount)
    {
        bool resourceTypeCheck = resource == null ? true : resourceType == resource;
        return resourceTypeCheck && ResourceAmount < maxResourceAmount;
    }

    public bool IsOutTransferPosibble(GameResource resourceType, int amount)
    {
        bool resourceTypeCheck = resourceType == null ? true : resourceType == resource;
        return resourceTypeCheck && ResourceAmount > 0;
    }

    //Transfer zasobów do plecaka
    public bool TransferIn(GameResource resourceType, int amount)
    {
        Debug.Log("Przesy³ zasobów start! próba!!!");
        if (IsInTransferPosibble(resourceType, amount))
        {
            resource = resourceType;
            gridCollection.prefab = resource.prefab;
            ResourceAmount += amount;
            Debug.Log("Przesy³ zasobów uda³o siê!!!");
            return true;
        }
        else
        {
            Debug.Log("Przesy³ zasobów nie uda³ siê :C");
        }
        return false;
    }
    //Transfer zasobów z plecaka
    public bool TransferOut(GameResource resourceType, int amount)
    {
        if (IsOutTransferPosibble(resourceType, amount))
        {
            ResourceAmount -= amount;
            if(ResourceAmount == 0)
            {
                resource = null;
                gridCollection.prefab = null;
            }
            return true;
        }
        return false;
    }

    private IEnumerator UpdateBackPackGridCollection()
    {
        while (this.ResourceAmount != gridCollection.gridObjects.Count)
        {
            if (this.ResourceAmount > gridCollection.gridObjects.Count)
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
