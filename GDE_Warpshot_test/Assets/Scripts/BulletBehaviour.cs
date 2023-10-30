using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BulletBehaviour : MonoBehaviour

{
    private Camera mainCam;
    private Vector3 mousePos;
    private Rigidbody2D bulletRb;
    private float force = 30f;
    private float destroyTime = 2.5f;
    private Vector2 aimDir;

    void Start()
    {
        bulletRb = gameObject.GetComponent<Rigidbody2D>();

        mainCam = Camera.main;
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        aimDir = mousePos - transform.position;
        bulletRb.velocity = new Vector2(aimDir.x, aimDir.y).normalized * force;

    }

    void Update()
    {
        // Destroys bullet after destroyTime has passed
        Destroy(gameObject, destroyTime);
    }
    
    // Destroys bullet and enemy on collision
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
