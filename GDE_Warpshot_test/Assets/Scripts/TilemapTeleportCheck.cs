using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TilemapTeleportCheck : MonoBehaviour
{
    private ItemTracker teleportTracker;

    void Start()
    {
        teleportTracker = GameObject.Find("Player").GetComponent<ItemTracker>();
    }

    // Player can teleport if the mouse is on a floor tile

    private void OnMouseOver()
    {
        teleportTracker.CanTeleport = true;
    }

    private void OnMouseExit()
    {
        teleportTracker.CanTeleport = false;
    }

}
