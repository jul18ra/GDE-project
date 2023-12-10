using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.Rendering;

public class Aiming : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject player;

    public Texture2D crosshair;
    private Vector2 cursorHotspot;
    public GameObject projectilePrefab;
    private float timer;

    private float fireRate = 0.3f;
    public float FireRate {  get { return fireRate; } set {  fireRate = value; } }


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
