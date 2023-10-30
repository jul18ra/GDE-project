using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public Texture2D crosshair;
    private Vector2 cursorHotspot;
    public Camera mainCam;
    private Vector3 mousePos;
    public GameObject projectilePrefab;
    private float fireRate = 0.2f;
    private float timer;
    void Start()
    {
        cursorHotspot = new Vector2 (crosshair.width / 2, crosshair.height / 2);
        Cursor.SetCursor(crosshair, cursorHotspot, CursorMode.ForceSoftware);
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Spawns a bullet
        if (Input.GetMouseButton(0) && timer > fireRate)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            timer = 0;
        }

    }

}
