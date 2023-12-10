using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    private PlayerController playerController;

    public GameObject gameOver;
    private GameOverScript gameOverScript;

    //private SpriteRenderer playerSprite;

    // private TMP_Text healthText;
    public Image healthBar;

    private float maxHealth = 10;
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

    private float currentHealth;
    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

    //private bool teleporting;
    //private bool flashing;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        // healthText = GameObject.Find("HealthText").GetComponent<TMP_Text>();


        gameOverScript = gameOver.GetComponent<GameOverScript>();

        //playerSprite = gameObject.GetComponent<SpriteRenderer>();
        playerController = gameObject.GetComponent<PlayerController>();
    }

    private void Update()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        //StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            // healthText.SetText($"Health: 0");
            gameOverScript.GameOver();
        }
    }


    /* IEnumerator FlashRed()
    {
        flashing = true;
        playerSprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        playerSprite.color = Color.white;
        flashing = false;
    }

    public bool getIsFlashing()
    {
        return flashing;
    }*/
}
