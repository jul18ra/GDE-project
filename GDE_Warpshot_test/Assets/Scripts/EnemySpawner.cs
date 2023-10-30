using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1.5f, 1.5f);
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, enemySpawner.transform.position, enemyPrefab.transform.rotation);
    }
}
