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
    public int MaxHealth { get { return maxHealth; } }

    private int currentHealth;
    public int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

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

    public void TakeDamage(int damage)
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
