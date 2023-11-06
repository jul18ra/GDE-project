using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BulletBehaviour : MonoBehaviour

{
    private Vector3 mousePos;
    private Rigidbody2D bulletRb;
    private float force = 30f;
    private float destroyTime = 2.5f;
    private Vector2 aimDir;
    private GameObject enemy;

    void Start()
    {
        bulletRb = gameObject.GetComponent<Rigidbody2D>();

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        aimDir = mousePos - transform.position;
        bulletRb.velocity = new Vector2(aimDir.x, aimDir.y).normalized * force;

    }

    void Update()
    {
        // Destroys bullet after destroyTime has passed
        Destroy(gameObject, destroyTime);
        enemy = GameObject.FindGameObjectWithTag("Enemy");

    }

    // Destroys bullet and enemy on collision
    void OnTriggerEnter2D(Collider2D enemy)
    {
        enemy.gameObject.GetComponent<EnemyHealth>().TakeDamage(2);
        Destroy(gameObject);
    }
}
