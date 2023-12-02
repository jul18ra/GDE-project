using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TilemapTeleportCheck : MonoBehaviour
{
    private ItemTracker itemTracker;

    void Start()
    {
        itemTracker = GameObject.Find("Player").GetComponent<ItemTracker>();
    }

    // Player can teleport if the mouse is on a floor tile

    private void OnMouseOver()
    {
        if (itemTracker.CurrentTeleports > 0)
        itemTracker.CanTeleport = true;
    }

    private void OnMouseExit()
    {
        itemTracker.CanTeleport = false;
    }

}
