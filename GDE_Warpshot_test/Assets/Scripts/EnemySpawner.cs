using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 2f, 2f);
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, enemySpawner.transform.position, enemyPrefab.transform.rotation);
    }
}
