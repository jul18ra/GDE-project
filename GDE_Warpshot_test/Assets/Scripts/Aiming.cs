using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class Aiming : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject player;

    public Texture2D crosshair;
    private Vector2 cursorHotspot;
    public GameObject projectilePrefab;
    private float fireRate = 0.2f;
    private float timer;
    public Quaternion gunRotation;

    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        cursorHotspot = new Vector2 (crosshair.width / 2, crosshair.height / 2);
        Cursor.SetCursor(crosshair, cursorHotspot, CursorMode.ForceSoftware);
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Spawns a bullet
        if (Input.GetMouseButton(0) && timer > fireRate && playerController.CanShoot)
        {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            timer = 0;
        }

    }

}
