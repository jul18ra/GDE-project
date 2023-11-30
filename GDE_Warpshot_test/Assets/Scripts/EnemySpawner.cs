using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class EnemySpawner : MonoBehaviour
{
    private List<GameObject> enemies;

    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;

    private float spawnStartTime = 2f;
    private float spawnRepeatRate = 1f;

    void Start()
    {
        enemies = new List<GameObject>
        {
            enemy1Prefab,
            enemy2Prefab
        };

        InvokeRepeating(nameof(SpawnEnemy), spawnStartTime, spawnRepeatRate);
    }

    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemies.Count);
        Instantiate(enemies[enemyIndex], gameObject.transform.position, enemies[enemyIndex].transform.rotation);
    }
}
