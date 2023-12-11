using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ItemTracker : MonoBehaviour
{
    private Image tpTileEmpty;
    private Image tpTileFull;
    private float tileSize = 140;
    private float maxSize;
    private float currentSize;

    private TMP_Text partText;

    private float maxTeleports = 3;
    public float MaxTeleports { get { return maxTeleports; } set { maxTeleports = value; } }

    private float currentTeleports;
    public float CurrentTeleports { get { return currentTeleports; } set { currentTeleports = value; } }

    private bool canTeleport = true;
    public bool CanTeleport { get { return canTeleport; } set { canTeleport = value; } }

    private int partAmount;
    public int PartAmount { get { return partAmount; } set { partAmount = value; } }



    void Start()
    {
        currentTeleports = maxTeleports;
        partText = GameObject.Find("PartText").GetComponent<TMP_Text>();
        tpTileEmpty = GameObject.Find("Empty").GetComponent<Image>();
        tpTileFull = GameObject.Find("Full").GetComponent<Image>();

        maxSize = maxTeleports * tileSize;
        currentSize = currentTeleports * tileSize;

        tpTileEmpty.rectTransform.sizeDelta = new Vector2(maxSize, tileSize);
        tpTileFull.rectTransform.sizeDelta = new Vector2(maxSize, tileSize);

        UpdatePartCount();

    }

    public void UpdateTeleportCount()
    {
        maxSize = maxTeleports * tileSize;
        currentSize = currentTeleports * tileSize;
        tpTileEmpty.rectTransform.sizeDelta = new Vector2(maxSize, tileSize);
        tpTileFull.rectTransform.sizeDelta = new Vector2(currentSize, tileSize);
    }

    public void UpdatePartCount()
    {
        partText.SetText($"{partAmount}");

    }

    // Update is called once per frame
    void Update()
    {
        if (currentTeleports <= 0)
        {
            canTeleport = false;
        }
    }



}
