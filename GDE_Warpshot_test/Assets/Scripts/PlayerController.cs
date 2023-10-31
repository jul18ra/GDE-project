using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float speed = 5f;
    private Vector2 movement;
    private Vector3 mousePos;

    public LayerMask hitLayers;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Teleports player to cursor position
        if (Input.GetMouseButtonDown(1))
        {
            gameObject.transform.position = mousePos;
        }

    }

    void FixedUpdate()
    {
        // Standard player movement
        playerRb.MovePosition(playerRb.position + speed * Time.fixedDeltaTime * movement.normalized);

    }
}

