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

    public List<SpriteRenderer> playerSprites;

    public Image healthBar;

    private float maxHealth = 10;
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

    private float currentHealth;
    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        gameOverScript = gameOver.GetComponent<GameOverScript>();

        playerController = gameObject.GetComponent<PlayerController>();

        UpdateHealthBar();
    }

    private void Update()
    {
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();
        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            gameOverScript.GameOver();
        }
    }

    public void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }


    private void ChangeColor(Color color)
    {
        foreach (SpriteRenderer sprite in playerSprites)
        {
            sprite.color = color;
        }
    }

    IEnumerator FlashRed()
    {
        ChangeColor(Color.red);
        yield return new WaitForSeconds(0.1f);
        ChangeColor(Color.white);
    }
}
