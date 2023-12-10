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

    private Animator playerAnim;

    public Rigidbody2D playerRb;
    private float moveSpeed = 10f;

    private Vector3 previousPos;
    public Vector3 PreviousPos { get { return previousPos; } }

    private Vector2 movement;
    private Vector3 mousePos;

    private bool teleporting;
    public bool Teleporting { get { return teleporting; } set { teleporting = value; } }

    private bool canShoot = true;
    public bool CanShoot { get { return canShoot; } set { canShoot = value; } }

    private void Start()
    {
        //playerSprite = gameObject.GetComponent<SpriteRenderer>();
        playerHealth = gameObject.GetComponent<PlayerHealth>();
        itemTracker = gameObject.GetComponent<ItemTracker>();
        playerAnim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (!teleporting)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        // Teleports player when right click 
        if (Input.GetMouseButtonDown(1) & itemTracker.CanTeleport)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            itemTracker.CurrentTeleports--;
            itemTracker.UpdateTeleportCount();
            teleporting = true;
            playerAnim.SetBool("isTeleporting", true);

        }
        else
        {
            playerAnim.SetBool("reachedDestination", true);
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

            if (other.CompareTag("HP") & playerHealth.CurrentHealth < playerHealth.MaxHealth)
            {
                playerHealth.CurrentHealth++;
                Destroy(other.gameObject);
            }

            if (other.CompareTag("TeleportItem") & itemTracker.CurrentTeleports < itemTracker.MaxTeleports)
            {
                itemTracker.CurrentTeleports++;
                itemTracker.UpdateTeleportCount();
                Destroy(other.gameObject);
            }

            if (other.CompareTag("EnemyPart"))
            {
                itemTracker.PartAmount++;
                Destroy(other.gameObject);
            }
        }
       
    }

    public void Teleport()
    {
        transform.position = mousePos;
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