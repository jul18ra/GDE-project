using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;
    Vector2 movement;

    public LayerMask hitLayers;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

    }

    void FixedUpdate()
    {
        // Standard player movement
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement.normalized);

        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Player teleports to cursor
        if (Physics.Raycast(mousePos, out hit, Mathf.Infinity, hitLayers))
        {
            gameObject.transform.position = hit.point;
        }
    }
}

