using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Enemy prefab to spawn
    [SerializeField] private float spawnInterval = 2f; // Time interval between spawns
    [SerializeField] private float spawnRadius = 10f; // Radius within which enemies spawn
    [SerializeField] private float awayFromPlayerSpawnDistance = 1f; // Minimum distance from player to spawn

    private GameObject player; // Reference to the player

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval); // Repeat enemy spawning at intervals
    }

    private void SpawnEnemy()
    {
        if (player != null)
        {
            Vector2 randomDirection = new Vector2(
                Random.Range(0, 2) == 0 ? -1 : 1,
                Random.Range(0, 2) == 0 ? -1 : 1
            );
            Vector2 awayFromPlayerOffset = randomDirection * awayFromPlayerSpawnDistance;
            Vector2 playerPosition = player.transform.position;
            Vector2 spawnPosition = playerPosition + awayFromPlayerOffset + Random.insideUnitCircle * spawnRadius;
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity); // Spawn enemy
        }
    }
}