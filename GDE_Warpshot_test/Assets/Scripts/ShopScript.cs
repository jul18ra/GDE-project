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
        DamageUpItem damageUp = new(bulletScript.BulletDamage, 5, $"Increase damage dealt by ");
        FireRateUpItem fireRateUp = new(aimingScript.FireRate, 5, $"Increase weapon fire rate by ");
        TeleportUpItem maxTeleportUp = new(itemScript.MaxTeleports, 5, $"Increase teleport item slots by ");
        HealthUpItem maxHealthUp = new(playerHealthScript.MaxHealth, 5, $"Increase max health by ");
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
            if (shopItems[n].GetType() != typeof(TeleportUpItem))
            {
                shopItems[n].RandomiseMultiplier();
                shopItems[n].Description += $"{shopItems[n].Multiplier * 100} %";
            }
            else
            {
                shopItems[n].Description += $"{shopItems[n].Multiplier}";
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
            itemCostText.SetText($"{shopItems[n].Cost} parts");
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
        enemySpawnerScript.WaveEnded = false;
        enemySpawnerScript.StartWave();

    }

    public void BuyItem(int itemIndex)
    {
        if(itemScript.PartAmount >= shopItems[itemIndex].Cost)
        {
            // Deduct cost from total part amount
            itemScript.PartAmount -= shopItems[itemIndex].Cost;

            shopItems[itemIndex].UpgradeStats();
            UpdateCost();
        }

    }
}