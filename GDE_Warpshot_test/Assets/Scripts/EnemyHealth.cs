using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int maxHealth = 10;
    private int currentHealth;
    private int damage = 2;
    private bool isDead;

    private Color origEnemyColor;
    private Color modifEnemyColor;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        origEnemyColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage()
    {
        currentHealth -= damage;
        ChangeColor(0.7f, Color.red);

        if(currentHealth < 1) 
        {
            isDead = true;
            Destroy(gameObject);
        }
    }

    void ChangeColor(float transVal, Color color)
    {
        modifEnemyColor = origEnemyColor;

        modifEnemyColor.a = transVal;
        modifEnemyColor = color;
        gameObject.GetComponent<SpriteRenderer>().color = modifEnemyColor;
    }
}
