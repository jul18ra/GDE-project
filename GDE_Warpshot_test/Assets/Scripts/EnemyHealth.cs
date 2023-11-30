using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject robotPartPrefab;

    private SpriteRenderer enemySprite;

    private GameObject textObj;

    private TMP_Text healthText;

    private int maxHealth;
    private int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.name == "Enemy1(Clone)")
        {
            maxHealth = 8;
        }

        if (gameObject.name == "Enemy2(Clone)")
        {
            maxHealth = 20;
        }

        currentHealth = maxHealth;

        textObj = gameObject.transform.GetChild(0).gameObject;
        healthText = textObj.GetComponent<TMP_Text>();
        healthText.SetText($"{currentHealth}/{maxHealth}");

        enemySprite = gameObject.GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthText.SetText($"{currentHealth}/{maxHealth}");
        StartCoroutine(FlashRed());

        if(currentHealth <= 0) 
        {
            Instantiate(robotPartPrefab, transform.position, transform.rotation);
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
