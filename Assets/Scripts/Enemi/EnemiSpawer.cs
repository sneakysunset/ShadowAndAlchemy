using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemiSpawer : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int numberOfEnemiesToSpawn = 5;
    public float spawnRadius = 5f;
    public float spawnInterval = 1f;
    public float waveInterval = 10f;

    public TMP_Text waveTimerText;

    private int enemiesSpawned = 0;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return StartCoroutine(SpawnEnemies());
            StartCoroutine(UpdateWaveTimer());
            yield return new WaitForSeconds(waveInterval);
            numberOfEnemiesToSpawn *= 2;
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (enemiesSpawned < numberOfEnemiesToSpawn)
        {
            SpawnEnemy();
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnInterval);
        }
        enemiesSpawned = 0;
    }

    private void SpawnEnemy()
    {
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        float angle = Random.Range(0f, Mathf.PI * 2f);
        Vector2 spawnPosition = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * spawnRadius;
        spawnPosition += (Vector2)transform.position;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private IEnumerator UpdateWaveTimer()
    {
        float timer = waveInterval;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            waveTimerText.text = "Next Wave In: " + Mathf.Ceil(timer).ToString() + "s";
            yield return null;
        }
        waveTimerText.text = "Wave Incoming!";
    }
}