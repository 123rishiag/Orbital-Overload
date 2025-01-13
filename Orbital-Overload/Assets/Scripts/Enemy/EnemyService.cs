using ServiceLocator.Bullet;
using ServiceLocator.Player;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Enemy
{
    public class EnemyService
    {
        // Private Variables
        private EnemyConfig enemyConfig;
        private List<EnemyController> enemies;
        private float enemySpawnTimer;

        // Private Services
        private BulletService bulletService;
        private PlayerService playerService;

        public EnemyService(EnemyConfig _enemyConfig, BulletService _bulletService, PlayerService _playerService)
        {
            // Setting Variables
            enemyConfig = _enemyConfig;
            enemies = new List<EnemyController>();

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

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                EnemyController enemy = enemies[i];
                if (enemy.GetEnemyView() != null)
                {
                    enemy.Update();
                }
                else
                {
                    enemies.RemoveAt(i); // Safely remove without affecting iteration
                }
            }
        }

        public void FixedUpdate()
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                EnemyController enemy = enemies[i];
                if (enemy.GetEnemyView() != null)
                {
                    enemy.FixedUpdate();
                }
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
            Vector2 playerPosition = playerService.GetPlayerController().GetPlayerView().GetPosition();
            Vector2 spawnPosition = playerPosition + awayFromPlayerOffset +
                Random.insideUnitCircle * enemyConfig.enemySpawnRadius;

            // Creating Controller
            EnemyController enemyController = new EnemyController(enemyConfig, spawnPosition,
                bulletService, playerService);
            enemies.Add(enemyController);
        }
    }
}