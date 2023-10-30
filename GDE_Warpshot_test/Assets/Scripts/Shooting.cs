using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    private Camera mainCam;
    private Vector3 mousePos;
    public Rigidbody2D rb;
    public float force;
    public float destroyTime;

    // Start is called before the first frame update
    void Start()
    {
        // FIX CAMERA + try to understand what you've written lol
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();    
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDir = mousePos - transform.position;
        rb.velocity = new Vector2(aimDir.x, aimDir.y).normalized * force; 
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, destroyTime);
    }
}
