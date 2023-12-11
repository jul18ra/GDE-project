using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private PlayerController playerController;
    private GameObject player;
    private bool canTakeDamage = true;
    private bool damageTimerRunning;

    // Start is called before the first frame update
    void Start()
    {
    }
    private void Update()
    {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerController = player.GetComponent<PlayerController>();

        if (playerController.Teleporting)
        {
            canTakeDamage = false;
        }
        else if(!damageTimerRunning)
        {
            canTakeDamage = true;

        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") & canTakeDamage)
        {
            if (gameObject.name == "RobotSpider(Clone)")
            {
                playerHealth.TakeDamage(1);
                StartCoroutine("DamageTimer");
            }

            if (gameObject.name == "RobotGiant(Clone)")
            {
                playerHealth.TakeDamage(1);
                StartCoroutine("DamageTimer");
            }
        }
    }

    private IEnumerator DamageTimer()
    {
        damageTimerRunning = true;  
        canTakeDamage = false;
        yield return new WaitForSeconds(0.7f);
        canTakeDamage = true;
        damageTimerRunning = false;
    }

}