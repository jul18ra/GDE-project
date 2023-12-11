using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public EnemySpawner enemySpawnerScript;

    private PlayerController playerController;

    private int gameOverScreen = 2;

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
            PlayerPrefs.SetInt("finalWaveCount", enemySpawnerScript.WaveCount);
            SceneManager.LoadScene(gameOverScreen);

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
