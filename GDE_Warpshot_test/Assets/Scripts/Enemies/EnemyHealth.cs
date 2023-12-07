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

    public GameObject robotPart;
    public GameObject tpItem;
    public GameObject hpItem;

    private float tpDropRate = 0.1f;
    private float hpDropRate = 0.1f;

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
            DropLoot();
            enemySpawnerScript.SpawnedEnemies--;
            Destroy(gameObject);
        }
    }

    private void DropRobotPart()
    {
        if (gameObject.name == "RobotSpider(Clone)")
        {
            Instantiate(robotPart, transform.position, transform.rotation);
        }

        if (gameObject.name == "RobotGiant(Clone)")
        {
            for (int i = 0; i < 2; i++)
            {
                Instantiate(robotPart, transform.position, transform.rotation);
            }
        }

    }

    private void DropItem(GameObject item, float dropRate)
    {
        if (Random.Range(0f, 1f) <= dropRate)
        {
            Instantiate(item, transform.position, transform.rotation);
        }
    }

    private void DropLoot()
    {
        DropRobotPart();
        DropItem(tpItem, tpDropRate);
        DropItem(hpItem, hpDropRate);
    }


    IEnumerator FlashRed()
    {
        enemySprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        enemySprite.color = Color.white;
    }
}
