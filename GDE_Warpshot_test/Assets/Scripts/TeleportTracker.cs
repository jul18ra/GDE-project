using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeleportTracker : MonoBehaviour
{
    private TMP_Text tpText;
    private int maxTeleports = 3;
    public int MaxTeleports { get { return currentTeleports; } }

    private int currentTeleports;
    public int CurrentTeleports { get { return currentTeleports; } set { currentTeleports = value; } }

    private bool canTeleport = true;
    public bool CanTeleport { get { return canTeleport; } set { canTeleport = value; } }


    // Start is called before the first frame update
    void Start()
    {
        currentTeleports = maxTeleports;
        tpText = GameObject.Find("TpText").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        tpText.SetText($"Teleports: {currentTeleports}");

        if (currentTeleports <= 0)
        {
            canTeleport = false;
        }
    }



}
