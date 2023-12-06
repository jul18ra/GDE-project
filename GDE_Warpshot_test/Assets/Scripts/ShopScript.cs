using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    private GameObject enemySpawners;
    private EnemySpawner enemySpawnerScript;

    public GameObject openShopPrompt;
    public GameObject shopUI;

    private bool shopIsOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawners = GameObject.Find("EnemySpawners");
        enemySpawnerScript = enemySpawners.GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySpawnerScript.WaveEnded & !shopIsOpen)
        {
            openShopPrompt.SetActive(true);
        }
        else
        {
            openShopPrompt.SetActive(false);
        }
    }

    public void OpenShop()
    {
        shopUI.SetActive(true);
        shopIsOpen = true;
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
        shopIsOpen = false;
    }
}
