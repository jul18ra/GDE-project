using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Item
{
    protected float upgrade;
    public float Upgrade { get { return upgrade; } }

    protected int cost;
    public int Cost { get { return cost; } set { cost = value; } }

    protected string description;

    public string Description { get { return description; } }

    protected string descriptionMultiplier;

    protected float multiplier;
    public float Multiplier { get { return multiplier; } }

    protected int timesPurchased = 1;

    protected float identifier;
    public float Indentifier { get { return identifier; } }

    public Item(float upgrade, int cost)
    {
        this.upgrade = upgrade;
        this.cost = cost;
    }
    public virtual void RandomiseMultiplier()
    {
        multiplier = UnityEngine.Random.Range(0.05f, 0.20f);
        multiplier = (float)Math.Round(multiplier, 2);
        descriptionMultiplier = $"{multiplier * 100} %";
        UpdateDescription();
    }
    protected virtual void UpdateDescription()
    {
    }

    public virtual void UpgradeStats()
    {
        upgrade += upgrade * multiplier;
    }
    public void RaiseCost()
    {
        timesPurchased++;
        cost *= timesPurchased;
    }

}

public class DamageUpItem : Item
{
    private GameObject shop;
    private ShopScript shopScript;
    private GameObject bullet;
    private BulletBehaviour bulletScript;

    public DamageUpItem(float upgrade, int cost) : base(upgrade, cost)
    {
        description = $"Damage dealt increased by {descriptionMultiplier}";
        identifier = 1;
    }

    public override void UpgradeStats()
    {
        shop = GameObject.Find("Shop");
        shopScript = shop.GetComponent<ShopScript>();
        bullet = shopScript.bullet;

        bulletScript = bullet.GetComponent<BulletBehaviour>();

        base.UpgradeStats();
        bulletScript.BulletDamage = upgrade;
    }

    protected override void UpdateDescription()
    {
        description = $"Damage dealt increased by {descriptionMultiplier}";
    }

}

public class FireRateUpItem : Item
{
    private GameObject gun;
    private Aiming aimingScript;

    public FireRateUpItem(float upgrade, int cost) : base(upgrade, cost)
    {
        description = $"Weapon fire rate increased by {descriptionMultiplier}";
        identifier = 2;
    }

    public override void UpgradeStats()
    {
        upgrade -= upgrade * multiplier;

        gun = GameObject.Find("GunNozzle");
        aimingScript = gun.GetComponent<Aiming>();

        aimingScript.FireRate = upgrade;
    }
    protected override void UpdateDescription()
    {
        description = $"Weapon fire rate increased by {descriptionMultiplier}";
    }
}

public class HealthUpItem : Item
{
    private GameObject healthBar;
    private GameObject player;
    private PlayerHealth playerHealthScript;

    private float startScaleX;

    public HealthUpItem(float upgrade, int cost) : base(upgrade, cost)
    {
        description = $"Max health increased by {descriptionMultiplier}";
        identifier = 3;
        startScaleX = 1;
    }

    public override void UpgradeStats()
    {
        base.UpgradeStats();

        player = GameObject.Find("Player");
        playerHealthScript = player.GetComponent<PlayerHealth>();
        healthBar = GameObject.Find("HealthBar");

        playerHealthScript.MaxHealth = upgrade;
        playerHealthScript.CurrentHealth = upgrade;
        Vector3 newScale = new Vector3((startScaleX += startScaleX * multiplier * 0.1f), 1, 1);
        healthBar.transform.localScale = newScale;
        startScaleX = newScale.x;
    }
    protected override void UpdateDescription()
    {
        description = $"Max health increased by {descriptionMultiplier}";
    }
}

public class HpDropRateUpItem : Item
{
    private GameObject shop;
    private ShopScript shopScript;
    private GameObject enemy;
    private LootDropScript lootDropScript;

    public HpDropRateUpItem(float upgrade, int cost) : base(upgrade, cost)
    {
        description = $"Teleport vial drop rate increased by {descriptionMultiplier}";
        identifier = 4;
    }
    public override void UpgradeStats()
    {
        base.UpgradeStats();

        shop = GameObject.Find("Shop");
        shopScript = shop.GetComponent<ShopScript>();
        enemy = shopScript.enemy;
        lootDropScript = enemy.GetComponent<LootDropScript>();

        lootDropScript.TpDropRate = upgrade;
    }
    protected override void UpdateDescription()
    {
        description = $"Teleport vial drop rate increased by {descriptionMultiplier}";
    }
}

public class TpDropRateUpItem : Item
{
    private GameObject shop;
    private ShopScript shopScript;
    private GameObject enemy;
    private LootDropScript lootDropScript;

    public TpDropRateUpItem(float upgrade, int cost) : base(upgrade, cost)
    {
        description = $"Health vial drop rate increased by {descriptionMultiplier}";
        identifier = 5;
    }
    public override void UpgradeStats()
    {
        base.UpgradeStats();

        shop = GameObject.Find("Shop");
        shopScript = shop.GetComponent<ShopScript>();
        enemy = shopScript.enemy;
        lootDropScript = enemy.GetComponent<LootDropScript>();

        lootDropScript.HpDropRate = upgrade;
    }
    protected override void UpdateDescription()
    {
        description = $"Health vial drop rate increased by {descriptionMultiplier}";
    }
}


public class TeleportUpItem : Item
{
    private GameObject player;
    private ItemTracker itemScript;
  
    public TeleportUpItem(float upgrade, int cost) : base(upgrade, cost)
    {
        multiplier = 1;
        description = "Increase teleport item slots by 1";
        identifier = 6;
    }

    public override void UpgradeStats()
    {
        player = GameObject.Find("Player");
        itemScript = player.GetComponent<ItemTracker>();

        upgrade += multiplier;
        itemScript.MaxTeleports = upgrade;
        itemScript.CurrentTeleports = upgrade;
        itemScript.UpdateTeleportCount();
    }

    public override void RandomiseMultiplier()
    {
        throw new NotSupportedException();
    }
}
