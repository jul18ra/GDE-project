using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRb;
    private float moveSpeed = 10f;
    private float teleportSpeed = 150f;

    private Vector2 movement;
    private Vector3 mousePos;
    private Color playerColor;

    private bool teleporting;

    private void Start()
    {
       //playerColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
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
            ChangeTransparency(0.5f);
            transform.position = Vector3.MoveTowards(transform.position, mousePos, Time.deltaTime * teleportSpeed);
        }
        else
        {
            teleporting = false;
            ChangeTransparency(1);
        }

    }

    void FixedUpdate()
    {
        // Standard player movement
        playerRb.MovePosition(playerRb.position + moveSpeed * Time.fixedDeltaTime * movement.normalized);

    }
    void ChangeTransparency(float transVal)
    {
        playerColor = gameObject.GetComponent<SpriteRenderer>().color;
        playerColor.a = transVal;
        gameObject.GetComponent<SpriteRenderer>().color = playerColor;
    }

}