using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private GameObject enemy;
    private GameObject textObj;

    private TMP_Text healthText;

    private int maxHealth = 10;
    private int currentHealth;
    //private int damage = 2;
    private bool isDead;

    private Color origEnemyColor;
    private Color modifEnemyColor;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        textObj = gameObject.transform.GetChild(0).gameObject;
        healthText = textObj.GetComponent<TMP_Text>();
        healthText.SetText($"{currentHealth}/{maxHealth}");

        origEnemyColor = gameObject.GetComponent<SpriteRenderer>().color;
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
        ChangeColor(0.7f, Color.red);

        if(currentHealth <= 0) 
        {
            isDead = true;
            Destroy(enemy.gameObject);
        }
    }

    void ChangeColor(float transVal, Color color)
    {
        modifEnemyColor = origEnemyColor;

        modifEnemyColor.a = transVal;
        modifEnemyColor = color;
        enemy.GetComponent<SpriteRenderer>().color = modifEnemyColor;
    }
}
