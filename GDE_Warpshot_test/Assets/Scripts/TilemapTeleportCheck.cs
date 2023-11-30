using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TilemapTeleportCheck : MonoBehaviour
{
    private TeleportTracker teleportTracker;

    void Start()
    {
        teleportTracker = GameObject.Find("Player").GetComponent<TeleportTracker>();
    }

    private void OnMouseOver()
    {
        teleportTracker.CanTeleport = true;
    }

    private void OnMouseExit()
    {
        teleportTracker.CanTeleport = false;
    }

}
