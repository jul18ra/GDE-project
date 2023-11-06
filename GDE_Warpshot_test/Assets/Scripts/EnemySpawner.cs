using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemySpawner;
    private float spawnStartTime = 2f;
    private float spawnRepeatRate = 1.5f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnStartTime, spawnRepeatRate);
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, enemySpawner.transform.position, enemyPrefab.transform.rotation);
    }
}
