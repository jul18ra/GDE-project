using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemySpawner;
    private float spawnStartTime = 5f;
    private float spawnRepeatRate = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnStartTime, spawnRepeatRate);
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, enemySpawner.transform.position, enemyPrefab.transform.rotation);
    }
}
