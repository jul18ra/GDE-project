using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed = 5f;
    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

    }
    // FixedUpdate is called every fixed framerate frame
    void FixedUpdate()
    {
        // adds speed + movement direction to current position
        rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * movement);
    }
}
