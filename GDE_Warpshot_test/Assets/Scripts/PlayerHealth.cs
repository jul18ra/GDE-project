using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    private PlayerController playerController;
    //private SpriteRenderer playerSprite;

    private TMP_Text healthText;

    private int maxHealth = 10;
    private int currentHealth;

    //private bool teleporting;
    //private bool flashing;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        healthText = GameObject.Find("HealthText").GetComponent<TMP_Text>();

        //playerSprite = gameObject.GetComponent<SpriteRenderer>();
        playerController = gameObject.GetComponent<PlayerController>();
    }

    private void Update()
    {
        healthText.SetText($"Health: {currentHealth}");
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //StartCoroutine(FlashRed());

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            healthText.SetText($"Health: 0");
            Time.timeScale = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!playerController.Teleporting & other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy");
            TakeDamage(2);
        }

        if (other.CompareTag("HP") & currentHealth < maxHealth)
        {
            currentHealth++;
            Destroy(other.gameObject);
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
