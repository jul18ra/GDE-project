using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    private AudioSource mainCamAudio;
    public AudioClip waveEndedMusic;
    public AudioClip waveOngoingMusic;
    public AudioClip warningSound;

    private bool musicPlaying = false;

    private List<Enemy> enemies;
    private List<GameObject> enemiesToSpawn = new();
    public List<Transform> spawners;
    private int spawnerIndex;

    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;

    private GameObject waveSplashScreen;
    private TMP_Text waveText;

    private int enemy1cost = 1;
    private int enemy2cost = 3;

    private int waveValue = 10;
    private int waveCount = 0;
    private int waveDuration = 30;
    private float waveTimer;
    private bool waveEnded;
    public bool WaveEnded { get { return waveEnded; } set { waveEnded = value; } }
    public int WaveCount { get { return waveCount; } }

    private float spawnTimer;
    private float spawnRepeatRate;
    private int spawnedEnemies;
    public int SpawnedEnemies { get { return spawnedEnemies; } set {  spawnedEnemies = value; } }

    void Start()
    {
        mainCamAudio = Camera.main.GetComponent<AudioSource>();
        waveSplashScreen = GameObject.Find("WaveSplashScreen");

        ListEnemies();
        StartWave();
    }

    private void Update()
    {
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

        if (waveTimer <= 0 & spawnedEnemies == 0 & enemiesToSpawn.Count == 0) 
        {
            waveEnded = true;
            PlayWaveEndedMusic();
        }
    }

    void PlayWaveEndedMusic()
    {
        if (waveEnded & !musicPlaying)
        {
            mainCamAudio.clip = waveEndedMusic;
            mainCamAudio.Play();
            musicPlaying = true;
        }
    }

    private IEnumerator PlayWarningSound()
    {
        mainCamAudio.clip = null;
        mainCamAudio.PlayOneShot(warningSound, 2);
        yield return new WaitForSeconds(warningSound.length);
        mainCamAudio.clip = waveOngoingMusic;
        mainCamAudio.Play();

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

    void GenerateEnemiesToSpawn()
    {
        waveValue += waveCount * 3;

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

    public void StartWave()
    {
        waveCount++;
        musicPlaying = false;
        StartCoroutine("DisplayWave");
        StartCoroutine("PlayWarningSound");
        GenerateEnemiesToSpawn();
        spawnRepeatRate = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
        spawnTimer = spawnRepeatRate;
    }

    private IEnumerator DisplayWave()
    {
        waveText = waveSplashScreen.GetComponentInChildren<TMP_Text>();
        waveText.SetText($"WAVE {waveCount}");
        yield return new WaitForSeconds(warningSound.length);
        waveText.SetText("");
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