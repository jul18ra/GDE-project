using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHead : MonoBehaviour
{
    public GameObject player;

    private float maxAngle = 15f;
    private float minAngle = -15f;

    private Vector2 lookDir;
    private Vector3 mousePos;
    private float rotation;

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDir = mousePos - transform.position;
        rotation = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rotation = Mathf.Clamp(rotation, minAngle, maxAngle);
        transform.rotation = Quaternion.Euler(0, 0, rotation);

    }
}
