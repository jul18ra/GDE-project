using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    private PlayerController playerController;
    private SpriteRenderer playerSprite;

    private GameObject enemy;

    private TMP_Text healthText;

    private int maxHealth = 10;
    private int currentHealth;

    private bool teleporting;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        healthText = GameObject.Find("HealthText").GetComponent<TMP_Text>();
        healthText.SetText($"Health: {currentHealth}");

        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        playerController = gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        teleporting = playerController.getIsTeleporting();

        Debug.Log(teleporting);
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthText.SetText($"Health: {currentHealth}");
        StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            healthText.SetText($"Health: 0");
            Time.timeScale = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D enemy)
    {
        if(!teleporting)
        {
            TakeDamage(2);

        }
    }

    IEnumerator FlashRed()
    {
        playerSprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        playerSprite.color = Color.white;
    }
}
