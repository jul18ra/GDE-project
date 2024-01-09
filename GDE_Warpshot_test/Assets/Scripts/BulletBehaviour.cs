using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

public class BulletBehaviour : MonoBehaviour

{
    private Vector3 mousePos;
    private Rigidbody2D bulletRb;
    private float force = 30f;
    private float destroyTime = 2.5f;
    private Vector2 aimDir;
    private Vector3 rotation;

    private float bulletDamage = 2;
    public float BulletDamage {  get { return bulletDamage; } set {  bulletDamage = value; } }

    void Start()
    {
        bulletRb = gameObject.GetComponent<Rigidbody2D>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDir = mousePos - transform.position;

        // Bullet angle and rotation when fired from gun
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        bulletRb.AddForce(aimDir.normalized * force, ForceMode2D.Impulse);
    }

    void Update()
    {
        // Destroys bullet after destroyTime has passed
        Destroy(gameObject, destroyTime);
    }

    // Destroys bullet and enemy on collision
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyHealth>(out var enemyHealth))
        {
            enemyHealth.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }

        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

}
