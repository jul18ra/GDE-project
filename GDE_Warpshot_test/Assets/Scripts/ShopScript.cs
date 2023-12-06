using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
        Item damageUp = new(bulletScript.BulletDamage, 5, 0.1f, $"Increase damage dealt by {0} %");
        Item fireRateUp = new(aimingScript.FireRate, 5, 0.1f, $"Increase weapon fire rate by {0} %");
        Item maxTeleportUp = new(itemScript.MaxTeleports, 5, 1, $"Increase teleport item slots by {0}");
        Item maxHealthUp = new(playerHealthScript.MaxHealth, 5, 0, $"Increase max health by {0} %");

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

        int n = 0;
        foreach (var itemText in itemTextList)
        {
            itemText.SetText(shopItems[n].description);
            n++;
        }

        n = 0;
        foreach (var itemCostText in itemCostTextList)
        {
            itemCostText.SetText($"{shopItems[n].cost}");
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

}

public class Item
{
    public float upgrade;
    public int cost;
    public float multiplier;
    public string description;

    public Item(float upgrade, int cost, float multiplier, string description) 
    {
        this.upgrade = upgrade;
        this.cost = cost;
        this.multiplier = multiplier;
        this.description = description;
    }

    // Make RiseCost() method
}
