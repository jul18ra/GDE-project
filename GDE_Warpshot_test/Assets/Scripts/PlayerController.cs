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
    private TeleportTracker teleportTracker;

    public Rigidbody2D playerRb;
    private float moveSpeed = 10f;
    private float teleportSpeed = 30f;

    private Vector3 previousPos;
    public Vector3 PreviousPos { get { return previousPos; } }

    private Vector2 movement;
    private Vector3 mousePos;

    //private SpriteRenderer playerSprite;
    private Color playerColor;

    private bool flashing;

    private bool teleporting;
    public bool Teleporting {  get { return teleporting; } }

    private bool canShoot = true;
    public bool CanShoot { get { return canShoot; } }

    private void Start()
    {
        //playerSprite = gameObject.GetComponent<SpriteRenderer>();
        playerHealth = gameObject.GetComponent<PlayerHealth>();
        teleportTracker = gameObject.GetComponent<TeleportTracker>();   
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Teleports player when right click 
        if (Input.GetMouseButtonDown(1) & teleportTracker.CanTeleport)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            teleportTracker.CurrentTeleports--;
            teleporting = true;
        }

        // Teleports player to cursor position
        if (teleporting & transform.position != mousePos)
        {
            //playerSprite.color = new Color(1f, 1f, 1f, 0.5f);
            transform.position = Vector3.MoveTowards(transform.position, mousePos, Time.deltaTime * teleportSpeed);
        }
        else
        {
            teleporting = false;
            previousPos = transform.position;
        }

        if (!flashing & !teleporting)
        {
            //playerSprite.color = new Color(1f, 1f, 1f, 1f);
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
        if (!teleporting & other.CompareTag("Enemy"))
        {
            playerHealth.TakeDamage(2);
        }

        if (other.CompareTag("HP") & playerHealth.CurrentHealth < playerHealth.MaxHealth)
        {
            playerHealth.CurrentHealth++;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("TP"))
        {
            teleportTracker.CurrentTeleports++;
            Destroy(other.gameObject);
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