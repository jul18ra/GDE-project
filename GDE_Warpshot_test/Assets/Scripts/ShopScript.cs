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
    public GameObject enemy;
    public GameObject bullet;

    private BulletBehaviour bulletScript;
    private Aiming aimingScript;
    private ItemTracker itemScript;
    private PlayerHealth playerHealthScript;
    private LootDropScript lootDropScript;

    private List<Item> items;
    private List<Item> shopItems = new();
    private List<Item> itemsCopy = new();

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
        ListItems();
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

        player = GameObject.Find("Player");
        gun = GameObject.Find("GunNozzle");

        bulletScript = bullet.GetComponent<BulletBehaviour>();
        aimingScript = gun.GetComponent<Aiming>();
        itemScript = player.GetComponent<ItemTracker>();
        playerHealthScript = player.GetComponent<PlayerHealth>();
        lootDropScript = enemy.GetComponent<LootDropScript>();
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
        DamageUpItem damageUp = new(bulletScript.BulletDamage, 5);
        FireRateUpItem fireRateUp = new(aimingScript.FireRate, 5);
        TeleportUpItem maxTeleportUp = new(itemScript.MaxTeleports, 5);
        HealthUpItem maxHealthUp = new(playerHealthScript.MaxHealth, 5);
        HpDropRateUpItem hpDropRateUp = new(lootDropScript.HpDropRate, 5);
        TpDropRateUpItem tpDropRateUp = new(lootDropScript.TpDropRate, 5);

        items = new List<Item>
        {
            damageUp,
            fireRateUp,
            maxTeleportUp,
            maxHealthUp,
            hpDropRateUp,
            tpDropRateUp,
        };
    }

    void CloneItemList()
    {
        foreach (Item item in items)
        {
            itemsCopy.Add(item);
        }
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
        CloneItemList();

        while (shopItems.Count <= 3)
        {
            int itemIndex = UnityEngine.Random.Range(0, itemsCopy.Count);
            shopItems.Add(itemsCopy[itemIndex]);
            itemsCopy.Remove(itemsCopy[itemIndex]);
        }

        itemsCopy.Clear();
    }

    void DisplayItems()
    {
        GetShopUIReferences();
        GenerateShopItems();

        int n = 0;
        foreach (var itemText in itemTextList)
        {
            if (shopItems[n].GetType() != typeof(TeleportUpItem))
            {
                shopItems[n].RandomiseMultiplier();
            }

            itemText.SetText(shopItems[n].Description);
            n++;
        }

        UpdateCost();

    }

    private void UpdateCost()
    {
        int n = 0;
        foreach (var itemCostText in itemCostTextList)
        {
            itemCostText.SetText($"{shopItems[n].Cost}");
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
        openShopPrompt.SetActive(true);
        NewWave();
    }

    private void NewWave()
    {
        enemySpawnerScript.WaveEnded = false;
        shopItems.Clear();
        enemySpawnerScript.StartWave();
    }

    public void BuyItem(int itemIndex)
    {
        if(itemScript.PartAmount >= shopItems[itemIndex].Cost)
        {
            // Deduct cost from total part amount
            itemScript.PartAmount -= shopItems[itemIndex].Cost;

            foreach (Item item in items) 
            { 
                if (shopItems[itemIndex].Indentifier == item.Indentifier)
                {
                    item.RaiseCost();
                }
            }

            shopItems[itemIndex].UpgradeStats();
            UpdateCost();
        }

    }
}