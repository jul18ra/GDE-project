using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;
using static Item;

public class ShopScript : MonoBehaviour
{
    private GameObject enemySpawners;
    private EnemySpawner enemySpawnerScript;

    private GameObject player;
    private GameObject gun;
    public GameObject bullet;

    private BulletBehaviour bulletScript;
    private Aiming aimingScript;
    private ItemTracker itemScript;
    private PlayerHealth playerHealthScript;

    private List<Item> items;
    private List<Item> shopItems = new();

    public GameObject openShopPrompt;
    public GameObject shopUI;

    private TMP_Text item1Text;
    private TMP_Text item2Text;
    private TMP_Text item3Text;

    private List<TMP_Text> itemTextList;

    private TMP_Text item1CostText;
    private TMP_Text item2CostText;
    private TMP_Text item3CostText;

    private List<TMP_Text> itemCostTextList;


    private bool shopIsOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
    }

    // Update is called once per frame
    void Update()
    {

        if (enemySpawnerScript.WaveEnded & !shopIsOpen)
        {
            openShopPrompt.SetActive(true);
        }
        else
        {
            openShopPrompt.SetActive(false);
        }
    }

    void GetReferences() 
    {
        enemySpawners = GameObject.Find("EnemySpawners");
        enemySpawnerScript = enemySpawners.GetComponent<EnemySpawner>();

        player = GameObject.FindWithTag("Player");
        gun = GameObject.Find("GunNozzle");

        bulletScript = bullet.GetComponent<BulletBehaviour>();
        aimingScript = gun.GetComponent<Aiming>();
        itemScript = player.GetComponent<ItemTracker>();
        playerHealthScript = player.GetComponent<PlayerHealth>();
    }

    void GetShopUIReferences()
    {
        item1Text = GameObject.Find("Image1").GetComponentInChildren<TMP_Text>();
        item2Text = GameObject.Find("Image2").GetComponentInChildren<TMP_Text>();
        item3Text = GameObject.Find("Image3").GetComponentInChildren<TMP_Text>();

        item1CostText = GameObject.Find("ButtonBuy1").GetComponentInChildren<TMP_Text>();
        item2CostText = GameObject.Find("ButtonBuy2").GetComponentInChildren<TMP_Text>();
        item3CostText = GameObject.Find("ButtonBuy3").GetComponentInChildren<TMP_Text>();

        ListText();
    }

    void ListItems()
    {
        Item damageUp = new(bulletScript.BulletDamage, 5, $"Increase damage dealt by ");
        Item fireRateUp = new(aimingScript.FireRate, 5, $"Increase weapon fire rate by ");
        Item maxTeleportUp = new(itemScript.MaxTeleports, 5, $"Increase teleport item slots by ");
        Item maxHealthUp = new(playerHealthScript.MaxHealth, 5, $"Increase max health by ");
        // Item dropRatehUp = new(script.DropRate, 5, $"Increase enemy drop rate by ");


        items = new List<Item>
        {
            damageUp,
            fireRateUp,
            maxTeleportUp,
            maxHealthUp
        };
    }

    void ListText()
    {
        itemTextList = new List<TMP_Text>
        {
            item1Text,
            item2Text,
            item3Text
        };

        itemCostTextList = new List<TMP_Text>
        {
            item1CostText, 
            item2CostText, 
            item3CostText
        };
    }

    void GenerateShopItems()
    {
        ListItems();

        while (shopItems.Count <= 3)
        {
            int itemIndex = UnityEngine.Random.Range(0, items.Count);
            shopItems.Add(items[itemIndex]);
            items.Remove(items[itemIndex]);
        }
    }

    void DisplayItems()
    {
        GetShopUIReferences();
        GenerateShopItems();
        ListItems();

        int n = 0;
        foreach (var itemText in itemTextList)
        {
            if (shopItems[n].description != items[2].description)
            {
                shopItems[n].RandomiseMultiplier();
                shopItems[n].description += $"{shopItems[n].multiplier * 100} %";
            }
            else
            {
                shopItems[n].SetMultiplier();
                shopItems[n].description += $"{shopItems[n].multiplier}";
            }

            itemText.SetText(shopItems[n].description);
            n++;
        }

        UpdateCost();

    }

    private void UpdateCost()
    {
        int n = 0;
        foreach (var itemCostText in itemCostTextList)
        {
            itemCostText.SetText($"{shopItems[n].cost} parts");
            n++;
        }
    }

    public void OpenShop()
    {
        shopUI.SetActive(true);
        DisplayItems();
        shopIsOpen = true;
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
        shopIsOpen = false;
    }

    public void BuyItem(int itemIndex)
    {
        if(itemScript.PartAmount >= shopItems[itemIndex].cost)
        {
            // Deduct cost from total part amount
            itemScript.PartAmount -= shopItems[itemIndex].cost;

            shopItems[itemIndex].UpgradeStats();
            UpdateCost();
        }
    }
}

public class Item
{
    public float upgrade;
    public int cost;
    public string description;
    private int timesPurchased = 1;
    public float multiplier;

    public Item(float upgrade, int cost, string description) 
    {
        this.upgrade = upgrade;
        this.cost = cost;
        this.description = description;
    }

    public void UpgradeStats()
    {
        if (multiplier % 1 == 0)
        {
            upgrade += multiplier;
        }
        else
        {
            upgrade += upgrade * multiplier;
        }

        RiseCost();
    }

    public void RandomiseMultiplier()
    {
        multiplier = UnityEngine.Random.Range(0.05f, 0.20f);
        multiplier = (float)Math.Round(multiplier, 2);
    }
    public void SetMultiplier()
    {
        multiplier = 1;
    }

    private void RiseCost() 
    {
        timesPurchased++;
        cost *= timesPurchased;
    }

}
