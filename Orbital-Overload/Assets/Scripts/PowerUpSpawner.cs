using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs; // Array of power-up prefabs
    public float spawnInterval = 5f;
    public float spawnRadius = 10f;
    public float awayFromPlayerSpawnDistance = 1f;

    private float timer;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnPowerUp();
            timer = 0f;
        }
    }

    void SpawnPowerUp()
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
            Instantiate(powerUpPrefabs[index], spawnPosition, Quaternion.identity);
        }
    }
}
