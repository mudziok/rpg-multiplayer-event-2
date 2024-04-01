using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject WoodTextPrefab;
    TMP_Text WoodText;
    public GameObject StoneTextPrefab;
    TMP_Text StoneText;

    PlayerBackPack backpack;

    public GameResource wood;
    public GameResource stone;

    GameObject treeGenerator;
    GameObject stoneGenerator1;
    GameObject stoneGenerator2;

    // Start is called before the first frame update
    void Start()
    {
        treeGenerator = GameObject.Find("TreeGenerator");
        stoneGenerator1 = GameObject.Find("UnfinishedStructure");
        stoneGenerator2 = GameObject.Find("UnfinishedStructure (1)");
    }

    public void SetLabels()
    {
        WoodText = WoodTextPrefab.GetComponent<TMP_Text>();
        StoneText = StoneTextPrefab.GetComponent<TMP_Text>();
        backpack = GameObject.Find("PlayerTransform").GetComponent<PlayerBackPack>();
        WoodText.text = "Wood: " + (backpack.resource == wood ? backpack.ResourceAmount.ToString() : '0');
        StoneText.text = "Stone: " + (backpack.resource == stone ? backpack.ResourceAmount.ToString() : '0');
    }

    public void UpgradeTreeGenerator()
    {
        if (backpack.resource == wood && backpack.ResourceAmount >= 5)
        {
            treeGenerator.GetComponent<ResourceGenerator>().generationAmount += 1;
            backpack.ResourceAmount -= 5;
            SetLabels();
        }
        
    }
}
