using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

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
        RaiseCost();
    }
    protected void RaiseCost()
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
    }

    public override void UpgradeStats()
    {
        base.UpgradeStats();

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
    private GameObject player;
    private PlayerHealth playerHealthScript;

    public HealthUpItem(float upgrade, int cost) : base(upgrade, cost)
    {
        description = $"Max health increased by {descriptionMultiplier}";
    }

    public override void UpgradeStats()
    {
        base.UpgradeStats();

        player = GameObject.FindWithTag("Player");
        playerHealthScript = player.GetComponent<PlayerHealth>();

        playerHealthScript.MaxHealth = upgrade;
        playerHealthScript.CurrentHealth = upgrade;
    }
    protected override void UpdateDescription()
    {
        description = $"Max health increased by {descriptionMultiplier}";
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
    }

    public override void UpgradeStats()
    {
        player = GameObject.FindWithTag("Player");
        itemScript = player.GetComponent<ItemTracker>();

        upgrade += multiplier;
        itemScript.MaxTeleports = upgrade;
        itemScript.CurrentTeleports = upgrade;
        RaiseCost();
    }

    public override void RandomiseMultiplier()
    {
        throw new NotSupportedException();
    }
}
