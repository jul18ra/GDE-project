using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;
    private Vector2 movement;
    private Vector3 mousePos;

    public LayerMask hitLayers;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(1))
        {
            gameObject.transform.position = mousePos;
        }

    }

    void FixedUpdate()
    {
        // Standard player movement
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement.normalized);

    }
}

