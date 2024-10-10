using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float timeBetweenEnemies = 1f;
    [SerializeField] private int baseEnemiesPerWave = 5;
    [SerializeField] private float difficultyScalingFactor = 0.1f;

    private int currentWave = 0;
    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return StartCoroutine(SpawnWave());
            
            while (activeEnemies.Count > 0)
            {
                yield return new WaitForSeconds(0.5f);
                activeEnemies.RemoveAll(enemy => enemy == null);
            }
            
            yield return new WaitForSeconds(timeBetweenWaves);
            
            currentWave++;
        }
    }

    private IEnumerator SpawnWave()
    {
        int enemiesToSpawn = CalculateEnemiesInWave();
        
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        
        GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPoint.position, spawnPoint.rotation);
        activeEnemies.Add(spawnedEnemy);
    }

    private int CalculateEnemiesInWave()
    {
        return Mathf.RoundToInt(baseEnemiesPerWave * (1 + currentWave * difficultyScalingFactor));
    }
}