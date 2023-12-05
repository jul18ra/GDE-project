using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class EnemySpawner : MonoBehaviour
{
    private List<Enemy> enemies;
    private List<GameObject> enemiesToSpawn = new();
    public List<Transform> spawners;
    private int spawnerIndex;

    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;

    private int enemy1cost = 1;
    private int enemy2cost = 3;

    private int waveValue;
    private int waveCount = 1;
    private int waveDuration = 10;
    private float waveTimer;

    private float spawnTimer;
    private float spawnRepeatRate;
    private int spawnedEnemies;
    
    public int SpawnedEnemies { get { return spawnedEnemies; } set {  spawnedEnemies = value; } }

    void Start()
    {
        StartWave();
    }

    private void Update()
    {
        Debug.Log(spawnedEnemies);

        if (spawnTimer <= 0)
        {
            if (enemiesToSpawn.Count > 0)
            {
                Instantiate(enemiesToSpawn[0], spawners[spawnerIndex].position, transform.rotation);

                spawnerIndex++;
                if (spawnerIndex > spawners.Count - 1)
                {
                    spawnerIndex = 0;
                }

                spawnedEnemies++;
                enemiesToSpawn.RemoveAt(0);
                spawnTimer = spawnRepeatRate;
            }

            else
            {
                waveTimer = 0;
            }

        }
        else
        {
            spawnTimer -= Time.deltaTime;
            waveTimer -= Time.deltaTime;
        }
    }

    void ListEnemies()
    {
        Enemy enemy1 = new(enemy1Prefab, enemy1cost);
        Enemy enemy2 = new(enemy2Prefab, enemy2cost);

        enemies = new List<Enemy>
        {
            enemy1,
            enemy2
        };
    }

    void PickEnemiesToSpawn()
    {
        ListEnemies();

        waveValue = waveCount * 10;

        while (waveValue > 0)
        {
            int enemyIndex = Random.Range(0, enemies.Count);

            if (waveValue - enemies[enemyIndex].cost >= 0)
            {
                enemiesToSpawn.Add(enemies[enemyIndex].enemyPrefab);
                waveValue -= enemies[enemyIndex].cost;
            }
        }
    }

    void StartWave()
    {
        PickEnemiesToSpawn();
        spawnRepeatRate = waveDuration - enemiesToSpawn.Count;
        waveTimer = waveDuration;
        spawnTimer = 2;
    }

}

public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;

    public Enemy(GameObject enemyPrefab, int cost) 
    {
        this.enemyPrefab = enemyPrefab;
        this.cost = cost;
    }     
}