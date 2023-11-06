using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private GameObject enemy;
    private SpriteRenderer enemySprite;

    private GameObject textObj;

    private TMP_Text healthText;

    private int maxHealth = 8;
    private int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        textObj = gameObject.transform.GetChild(0).gameObject;
        healthText = textObj.GetComponent<TMP_Text>();
        healthText.SetText($"{currentHealth}/{maxHealth}");

        enemySprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        enemy = gameObject;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthText.SetText($"{currentHealth}/{maxHealth}");
        StartCoroutine(FlashRed());

        if(currentHealth <= 0) 
        {
            Destroy(enemy.gameObject);
        }
    }

    IEnumerator FlashRed()
    {
        enemySprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        enemySprite.color = Color.white;
    }
}
