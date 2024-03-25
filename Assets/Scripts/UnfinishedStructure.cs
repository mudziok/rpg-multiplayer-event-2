using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

//Komponent s³u¿¹cdo budowy struktur
public class UnfinishedStructure : MonoBehaviour
{
    //Zmienne z Property Notify
    //Iloœæ zasobu w budynku
    private int resourceAmount = 0;

    //Tutaj mamy RaisePropertyChanged po to by móc poinformowaæ o zmianie iloœci zasobów - taki databinding
    public int ResourceAmount
    {
        get => resourceAmount;
        set
        {
            resourceAmount = value;
            RaisePropertyChanged("ResourceAmount");
        }
    }

    //Element aktywuj¹cy generator
    public InteractiveElement activator;
    //Zasób potrzebny do zbudowania struktury
    public GameResource neededResource;
    //Prefab zbudowanej struktury
    public GameObject finishedStructurePrefab;
    //Miejsce gdzie ma byæ zbudowana struktura
    public Transform finishedStructureLocation;
    //Iloœæ zasobu potrzebnego do zbudowania struktury
    public int neededResourceAmount;
    [SerializeField]
    //Czy trwa transfer zasobów - aktywuje siê to kiedy gracz wejdzie na p³ytkê aktywacyjn¹ / uruchomi aktywator
    private bool inTransferActivated;
    [SerializeField]
    //Timer do transferu zasobów - bo tranfer nie jest automatyczny tylko odbywa siê co jakiœ czas z opóŸnieniem
    private float inTransferTimer = 0f;
    [SerializeField]
    //OpóŸnienie transferu zasobów - to co wy¿ej
    private float inTransferDelay = 2f;
    [SerializeField]
    //Kolekcja zasobów w budynku, grid do po³o¿enia zasobów obok budynku
    private GridGameObjectCollection resourcesDeposidGridCollection;

    [SerializeField]
    private GameObject minigamePrefab;
    private MinigameBase minigame;

    public event PropertyChangedEventHandler PropertyChanged;

    private void Awake()
    {
        //Subskrybujemy siê na zmiany w aktywatorze
        activator.PropertyChanged += OnPropertyChanged;
        //Subskrybujemy siê na zmiany w zasobach
        this.PropertyChanged += OnPropertyChanged;
        //Ustawiamy prefab zasobu w gridzie
        resourcesDeposidGridCollection.prefab = neededResource.prefab;
    }

    private void Update()
    {
        //Jeœli aktywator transferu jest aktywowany i iloœæ zasobów jest mniejsza ni¿ potrzebna iloœæ zasobów to zaczynamy transfer
        if (inTransferActivated && ResourceAmount < neededResourceAmount)
        {
            inTransferTimer += Time.deltaTime;
            if (inTransferTimer >= inTransferDelay)
            {
                inTransferTimer = 0f;
                if (activator.PlayerGameObject.GetComponent<PlayerBackPack>().TransferOut(neededResource, 1))
                {
                    ResourceAmount += 1;
                }
                if (CheckIfCanBuild())
                {
                    minigame = MinigamesManager.Instance.StartMinigame(minigamePrefab);
                    minigame.actionPerformedEvent += BuildStructure;
                    minigame.closedEvent += () => minigame = null;
                }
            }
        }
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "IsActivated")
        {
            inTransferActivated = activator.IsActivated;
        }

        if(e.PropertyName == "ResourceAmount")
        {
            StartCoroutine(UpdateDepotStash());
        }
    }

    private void RaisePropertyChanged(string propertyName)
    {
        var propChange = PropertyChanged;
        if (propChange == null) return;
        propChange(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool CheckIfCanBuild()
    {
        if (neededResource == null)
        {
            Debug.LogError("Needed resource is not set");
            return false;
        }
        if (ResourceAmount < neededResourceAmount)
        {
            return false;
        }
        return true;
    }

    private void BuildStructure()
    {
        Instantiate(finishedStructurePrefab, finishedStructureLocation.position, finishedStructureLocation.rotation);
        Destroy(gameObject);
    }

    private IEnumerator UpdateDepotStash()
    {
        while (ResourceAmount != resourcesDeposidGridCollection.gridObjects.Count)
        {
            if (ResourceAmount > resourcesDeposidGridCollection.gridObjects.Count)
            {
                resourcesDeposidGridCollection.AddObjectToGrid();
            }
            else
            {
                resourcesDeposidGridCollection.RemoveObjectFromGrid();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}

