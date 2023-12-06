using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemTracker : MonoBehaviour
{
    private TMP_Text tpText;
    private TMP_Text partText;

    private int maxTeleports = 3;
    public int MaxTeleports { get { return maxTeleports; } set { maxTeleports = value; } }

    private int currentTeleports;
    public int CurrentTeleports { get { return currentTeleports; } set { currentTeleports = value; } }

    private bool canTeleport = true;
    public bool CanTeleport { get { return canTeleport; } set { canTeleport = value; } }

    private int partAmount;
    public int PartAmount { get { return partAmount; } set { partAmount = value; } }


    void Start()
    {
        currentTeleports = maxTeleports;
        tpText = GameObject.Find("TpText").GetComponent<TMP_Text>();
        partText = GameObject.Find("PartText").GetComponent<TMP_Text>();

    }

    // Update is called once per frame
    void Update()
    {
        tpText.SetText($"Teleports: {currentTeleports}");
        partText.SetText($"Parts: {partAmount}");

        if (currentTeleports <= 0)
        {
            canTeleport = false;
        }
    }



}
