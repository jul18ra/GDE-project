using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public GameObject player;

    private float scale = 0.7f;
    private Vector3 defScale;

    private Vector2 lookDir;
    private Vector3 mousePos;
    private float rotation;

    private Quaternion gunRotation;

    private void Start()
    {
        defScale = new Vector3(scale, scale, scale);
    }
    void Update()
    {
        // Rotates gun to face the mouse 
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDir = mousePos - transform.position;
        rotation = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation);

        gunRotation = transform.rotation;

        // Makes the player face left if the cursor is on the left side of the player
        if (rotation < -90 | rotation > 90)
        {
            player.transform.localScale = new Vector3(defScale.x * -1, defScale.y, defScale.z);
            // Flips the gun
            transform.localRotation = Quaternion.Euler(180, 180, -rotation);
        }
        else
        {
            player.transform.localScale = new Vector3(defScale.x, defScale.y, defScale.z);
        }

    }
    public Quaternion GunRotation
    {
        get { return gunRotation; }
    }
}
