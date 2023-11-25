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

    public Rigidbody2D playerRb;
    private float moveSpeed = 10f;
    private float teleportSpeed = 30f;

    private Vector3 previousPos;
    private Vector2 movement;
    private Vector3 mousePos;

    //private SpriteRenderer playerSprite;
    private Color playerColor;

    private bool teleporting;
    private bool flashing;

    private void Start()
    {
        //playerSprite = gameObject.GetComponent<SpriteRenderer>();
        playerHealth = gameObject.GetComponent<PlayerHealth>();

    }

    void Update()
    {
        //flashing = playerHealth.getIsFlashing();

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Teleports player when right click 
        if (Input.GetMouseButtonDown(1))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

    public bool getIsTeleporting()
    {
        return teleporting;
    }

    public Vector3 getPreviousPos()
    {
        return previousPos;
    }
}