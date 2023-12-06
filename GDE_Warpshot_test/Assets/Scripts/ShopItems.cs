using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    protected float upgrade;
    public float Upgrade { get { return upgrade; } }

    protected int cost;
    public int Cost { get { return cost; } set { cost = value; } }

    protected string description;

    public string Description { get { return description; } set { description = value; } }

    protected float multiplier;
    public float Multiplier { get { return multiplier; } }

    protected int timesPurchased = 1;


    public Item(float upgrade, int cost, string description)
    {
        this.upgrade = upgrade;
        this.cost = cost;
        this.description = description;
    }
    public virtual void RandomiseMultiplier()
    {
        multiplier = UnityEngine.Random.Range(0.05f, 0.20f);
        multiplier = (float)Math.Round(multiplier, 2);
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

    public DamageUpItem(float upgrade, int cost, string description) : base(upgrade, cost, description)
    {
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
}

public class FireRateUpItem : Item
{
    private GameObject gun;
    private Aiming aimingScript;

    public FireRateUpItem(float upgrade, int cost, string description) : base(upgrade, cost, description)
    {
    }

    public override void UpgradeStats()
    {
        base.UpgradeStats();

        gun = GameObject.Find("GunNozzle");
        aimingScript = gun.GetComponent<Aiming>();

        aimingScript.FireRate = upgrade;
    }
}

public class HealthUpItem : Item
{
    private GameObject player;
    private PlayerHealth playerHealthScript;

    public HealthUpItem(float upgrade, int cost, string description) : base(upgrade, cost, description)
    {
    }

    public override void UpgradeStats()
    {
        base.UpgradeStats();

        player = GameObject.FindWithTag("Player");
        playerHealthScript = player.GetComponent<PlayerHealth>();

        playerHealthScript.MaxHealth = upgrade;
    }
}

public class TeleportUpItem : Item
{
    private GameObject player;
    private ItemTracker itemScript;
  
    public TeleportUpItem(float upgrade, int cost, string description) : base(upgrade, cost, description)
    {
        multiplier = 1;
    }

    public override void UpgradeStats()
    {
        player = GameObject.FindWithTag("Player");
        itemScript = player.GetComponent<ItemTracker>();

        upgrade += multiplier;
        itemScript.MaxTeleports = upgrade;
        RiseCost();
    }

    public override void RandomiseMultiplier()
    {
        throw new NotSupportedException();
    }
}
