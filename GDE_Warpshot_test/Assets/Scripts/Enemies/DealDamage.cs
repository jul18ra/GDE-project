using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private GameObject player;
    private bool canTakeDamage = true;

    // Start is called before the first frame update
    void Start()
    {
    }
    private void Update()
    {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") & canTakeDamage)
        {
            if (gameObject.name == "RobotSpider(Clone)")
            {
                playerHealth.TakeDamage(1);
                StartCoroutine("damageTimer");
            }

            if (gameObject.name == "RobotGiant(Clone)")
            {
                playerHealth.TakeDamage(1);
                StartCoroutine("damageTimer");
            }
        }
    }

    private IEnumerator damageTimer()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.7f);
        canTakeDamage = true;
    }

}