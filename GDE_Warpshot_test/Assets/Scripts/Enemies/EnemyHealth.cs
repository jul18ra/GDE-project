using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class EnemyHealth : MonoBehaviour
{
    private GameObject enemySpawners;
    private EnemySpawner enemySpawnerScript;
    private LootDropScript lootDropScript;

    private SpriteRenderer enemySprite;

    private GameObject textObj;

    private TMP_Text healthText;

    private float maxHealth;
    private float currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        enemySpawners = GameObject.Find("EnemySpawners");
        enemySpawnerScript = enemySpawners.GetComponent<EnemySpawner>();
        lootDropScript = gameObject.GetComponent<LootDropScript>();

        if (gameObject.name == "RobotSpider(Clone)")
        {
            maxHealth = 8;
        }

        if (gameObject.name == "RobotGiant(Clone)")
        {
            maxHealth = 20;
        }

        currentHealth = maxHealth;

        textObj = gameObject.transform.GetChild(0).gameObject;
        healthText = textObj.GetComponent<TMP_Text>();
        healthText.SetText($"{currentHealth}/{maxHealth}");

        enemySprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthText.SetText($"{currentHealth}/{maxHealth}");
        StartCoroutine(FlashRed());

        if(currentHealth <= 0) 
        {
            lootDropScript.DropLoot();
            enemySpawnerScript.SpawnedEnemies--;
            Destroy(gameObject);
        }
    }

    IEnumerator FlashRed()
    {
        enemySprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        enemySprite.color = Color.white;
    }
}
