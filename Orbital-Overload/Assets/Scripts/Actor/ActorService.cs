using ServiceLocator.Bullet;
using ServiceLocator.Sound;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Actor
{
    public class ActorService
    {
        // Private Variables
        private ActorConfig actorConfig;
        private ActorController playerActorController;
        private List<ActorController> enemyActorControllers;
        private float enemySpawnTimer;

        // Private Services
        private SoundService soundService;
        private BulletService bulletService;

        public ActorService(ActorConfig _actorConfig,
            SoundService _soundService, BulletService _bulletService)
        {
            // Setting Variables
            actorConfig = _actorConfig;
            enemyActorControllers = new List<ActorController>();

            // Setting Services
            soundService = _soundService;
            bulletService = _bulletService;

            // Setting Elements
            CreatePlayer();
            enemySpawnTimer = _actorConfig.enemySpawnInterval;
        }

        private void CreatePlayer()
        {
            // Fetching Random Index
            int actorIndex = Random.Range(0, actorConfig.enemyData.Length);

            // Fetching Spawn Position
            Vector2 spawnPosition = new Vector2(0f, 0f);
            playerActorController = new PlayerActorController(
                actorConfig, spawnPosition, actorIndex,
                soundService, bulletService, this);
        }

        private void CreateEnemy()
        {
            // Fetching Random Index
            int actorIndex = Random.Range(0, actorConfig.enemyData.Length);

            // Fetching Spawn Position
            Vector2 randomDirection = new Vector2(
                    Random.Range(0, 2) == 0 ? -1 : 1,
                    Random.Range(0, 2) == 0 ? -1 : 1
                    );
            Vector2 awayFromPlayerOffset = randomDirection * actorConfig.enemyAwayFromPlayerSpawnDistance;
            Vector2 playerPosition = playerActorController.GetActorView().GetPosition();
            Vector2 spawnPosition = playerPosition + awayFromPlayerOffset +
                Random.insideUnitCircle * actorConfig.enemySpawnRadius;

            // Creating Controller
            ActorController enemyActorController = new EnemyActorController(
                actorConfig, spawnPosition, actorIndex,
                soundService, bulletService, this);
            enemyActorControllers.Add(enemyActorController);
        }

        public void Update()
        {
            SpawnEnemy();
            ProcessActorUpdate();
        }
        public void FixedUpdate()
        {
            ProcessActorFixedUpdate();
        }

        private void ProcessActorUpdate()
        {
            // For Player
            playerActorController.Update();

            // For Enemies
            for (int i = enemyActorControllers.Count - 1; i >= 0; i--)
            {
                ActorController enemy = enemyActorControllers[i];
                if (enemy.IsAlive())
                {
                    enemy.Update();
                }
                else
                {
                    enemyActorControllers.RemoveAt(i); // Safely removing destroyed enemies from List
                }
            }
        }
        private void ProcessActorFixedUpdate()
        {
            // For Player
            playerActorController.FixedUpdate();

            // For Enemies
            for (int i = enemyActorControllers.Count - 1; i >= 0; i--)
            {
                ActorController enemy = enemyActorControllers[i];
                if (enemy.IsAlive())
                {
                    enemy.FixedUpdate();
                }
            }
        }

        private void SpawnEnemy()
        {
            // Accumulate time
            enemySpawnTimer -= Time.deltaTime;

            // Check if the spawn interval has passed
            if (enemySpawnTimer < 0)
            {
                enemySpawnTimer = actorConfig.enemySpawnInterval; // Reset the timer
                CreateEnemy();
            }
        }

        // Getters
        public ActorController GetPlayerActorController() => playerActorController;
        public List<ActorController> GetEnemyActorControllers() => enemyActorControllers;
    }
}