using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float spawnRadius = 10f;
    public float awayFromPlayerSpawnDistance = 1f;
    private GameObject player;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (player != null)
        {
            Vector2 randomDirection = new Vector2(
                Random.Range(0, 2) == 0 ? -1 : 1,
                Random.Range(0, 2) == 0 ? -1 : 1
            );
            Vector2 awaFromPlayerOffset = randomDirection * awayFromPlayerSpawnDistance;
            Vector2 playerPosition = player.transform.position;
            Vector2 spawnPosition = playerPosition + awaFromPlayerOffset + Random.insideUnitCircle * spawnRadius;
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
