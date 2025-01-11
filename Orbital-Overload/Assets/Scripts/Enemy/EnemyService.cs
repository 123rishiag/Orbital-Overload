using ServiceLocator.Bullet;
using ServiceLocator.Player;
using UnityEngine;

namespace ServiceLocator.Enemy
{
    public class EnemyService
    {
        // Private Variables
        private EnemyConfig enemyConfig;
        private float enemySpawnTimer;

        // Private Services
        private BulletService bulletService;
        private PlayerService playerService;

        public EnemyService(EnemyConfig _enemyConfig, BulletService _bulletService, PlayerService _playerService)
        {
            // Setting Variables
            enemyConfig = _enemyConfig;

            // Setting Services
            bulletService = _bulletService;
            playerService = _playerService;

            // Setting Elements
            enemySpawnTimer = enemyConfig.enemySpawnInterval;
        }

        public void Update()
        {
            // Accumulate time
            enemySpawnTimer -= Time.deltaTime;

            // Check if the spawn interval has passed
            if (enemySpawnTimer < 0)
            {
                enemySpawnTimer = enemyConfig.enemySpawnInterval; // Reset the timer
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            // Fetching Data
            EnemyData enemyData = enemyConfig.enemyData;

            // Fetching Position & Direction
            Vector2 randomDirection = new Vector2(
                    Random.Range(0, 2) == 0 ? -1 : 1,
                    Random.Range(0, 2) == 0 ? -1 : 1
                    );
            Vector2 awayFromPlayerOffset = randomDirection * enemyConfig.enemyAwayFromPlayerSpawnDistance;
            Vector2 playerPosition = playerService.GetPlayerController().GetPosition();
            Vector2 spawnPosition = playerPosition + awayFromPlayerOffset +
                Random.insideUnitCircle * enemyConfig.enemySpawnRadius;

            // Instantiating Object
            GameObject enemy = GameObject.Instantiate(enemyConfig.enemyPrefab, spawnPosition, Quaternion.identity);

            // Creating Controller
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.Init(enemyData, bulletService, playerService);
        }
    }
}