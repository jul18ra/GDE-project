using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // Player teleports to cursor
        if (Input.GetMouseButtonDown(1))
        {
            gameObject.transform.position = mousePos;
        }
    }
}
