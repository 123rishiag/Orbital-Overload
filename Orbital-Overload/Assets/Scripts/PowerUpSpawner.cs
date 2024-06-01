using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUpPrefabs; // Array of power-up prefabs
    [SerializeField] private float spawnInterval = 5f; // Time interval between spawns
    [SerializeField] private float spawnRadius = 10f; // Radius within which power-ups spawn
    [SerializeField] private float awayFromPlayerSpawnDistance = 1f; // Minimum distance from player to spawn

    private GameObject player; // Reference to the player

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("SpawnPowerUp", spawnInterval, spawnInterval); // Repeat Powerup spawning at intervals
    }

    private void SpawnPowerUp()
    {
        if (player != null)
        {
            Vector2 randomDirection = new Vector2(
                Random.Range(0, 2) == 0 ? -1 : 1,
                Random.Range(0, 2) == 0 ? -1 : 1
            );
            int index = Random.Range(0, powerUpPrefabs.Length);
            Vector2 awayFromPlayerOffset = randomDirection * awayFromPlayerSpawnDistance;
            Vector2 playerPosition = player.transform.position;
            Vector2 spawnPosition = playerPosition + awayFromPlayerOffset + Random.insideUnitCircle * spawnRadius;
            Instantiate(powerUpPrefabs[index], spawnPosition, Quaternion.identity); // Spawn power-up
        }
    }
}