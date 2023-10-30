using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public Camera mainCam;
    private Vector3 mousePos;
    public GameObject projectilePrefab;
    private float fireRate = 0.2f;
    private float timer;


    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // Calculation of vector that points from player to mouse position
        Vector2 aimDir = mousePos - transform.position;

        // Angle of rotation + convertion from rad to deg
        float rotation = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

        // Returns a rotation that rotates "rotation" degrees around the z axis
        transform.rotation = Quaternion.Euler(0, 0, rotation);

        timer += Time.deltaTime;

        // Spawns a bullet
        if (Input.GetMouseButton(0) && timer > fireRate)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            timer = 0;
        }


    }

}
