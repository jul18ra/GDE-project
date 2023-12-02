using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Profiling;
using UnityEngine;
using UnityEngine.XR;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerController : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private ItemTracker itemTracker;

    public Rigidbody2D playerRb;
    private float moveSpeed = 10f;
    private float teleportSpeed = 30f;

    private Vector3 previousPos;
    public Vector3 PreviousPos { get { return previousPos; } }

    private Vector2 movement;
    private Vector3 mousePos;

    private bool teleporting;
    public bool Teleporting {  get { return teleporting; } }

    private bool canShoot = true;
    public bool CanShoot { get { return canShoot; } }

    private void Start()
    {
        //playerSprite = gameObject.GetComponent<SpriteRenderer>();
        playerHealth = gameObject.GetComponent<PlayerHealth>();
        itemTracker = gameObject.GetComponent<ItemTracker>();   
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Teleports player when right click 
        if (Input.GetMouseButtonDown(1) & itemTracker.CanTeleport)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            itemTracker.CurrentTeleports--;
            teleporting = true;
        }

        // Teleports player to cursor position
        if (teleporting & transform.position != mousePos)
        {
            transform.position = Vector3.MoveTowards(transform.position, mousePos, Time.deltaTime * teleportSpeed);
        }
        else
        {
            teleporting = false;
            previousPos = transform.position;
        }

    }

    void FixedUpdate()
    {
        // Standard player movement
        if(!teleporting)
        {
            playerRb.MovePosition(playerRb.position + moveSpeed * Time.fixedDeltaTime * movement.normalized);

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!teleporting)
        {
            if (other.CompareTag("Enemy"))
            {
                playerHealth.TakeDamage(2);
            }

            if (other.CompareTag("HP") & playerHealth.CurrentHealth < playerHealth.MaxHealth)
            {
                playerHealth.CurrentHealth++;
                Destroy(other.gameObject);
            }

            if (other.CompareTag("TeleportItem") & itemTracker.CurrentTeleports < itemTracker.MaxTeleports)
            {
                itemTracker.CurrentTeleports++;
                Destroy(other.gameObject);
            }

            if (other.CompareTag("EnemyPart"))
            {
                itemTracker.PartAmount++;
                Destroy(other.gameObject);
            }
        }
       
    }

    private void OnMouseOver()
    {
        canShoot = false;
    }

    private void OnMouseExit()
    {
        canShoot = true;
    }

}