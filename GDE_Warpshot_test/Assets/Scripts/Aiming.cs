using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public Camera mainCam;
    private Vector3 mousePos;
    public GameObject projectilePrefab;

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // Calculation of vector that points from player to mouse position
        Vector2 aimDir = mousePos - transform.position;

        // Angle of rotation + convertion from rad to deg
        float rotation = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

        // Returns a rotation that rotates "rotation" degrees around the z axis
        transform.rotation = Quaternion.Euler(0, 0, rotation);

        // Spawns a bullet
        if (Input.GetMouseButton(0))
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }

    }
       
}
