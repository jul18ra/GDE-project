using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Rigidbody2D playerRb;
    private GameObject player;

    private GameObject crosshair;
    private Vector3 crosshairPos;

    // Start is called before the first frame update
    void Start()
    {
        crosshair = GameObject.Find("Crosshair");
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        crosshairPos = crosshair.transform.position;

        if (Input.GetMouseButtonDown(1))
        {
            player.transform.position = crosshairPos;
        }
    }
}
