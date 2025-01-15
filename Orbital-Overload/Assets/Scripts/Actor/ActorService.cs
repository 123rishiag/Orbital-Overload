using ServiceLocator.Projectile;
using ServiceLocator.Sound;
using ServiceLocator.Spawn;
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

        // Private Services
        private SoundService soundService;
        private SpawnService spawnService;
        private ProjectileService projectileService;

        public ActorService(ActorConfig _actorConfig)
        {
            // Setting Variables
            actorConfig = _actorConfig;
            enemyActorControllers = new List<ActorController>();
        }

        public void Init(SoundService _soundService, SpawnService _spawnService, ProjectileService _projectileService)
        {
            // Setting Services
            soundService = _soundService;
            spawnService = _spawnService;
            projectileService = _projectileService;

            // Creating spawn controller for enemies
            spawnService.CreateSpawnController(actorConfig.enemySpawnInterval, actorConfig.enemySpawnRadius,
                actorConfig.enemyAwayFromPlayerSpawnDistance, CreateEnemy);

            // Setting Elements
            CreatePlayer();
        }

        private void CreatePlayer()
        {
            // Fetching Random Index
            int actorIndex = Random.Range(0, actorConfig.enemyData.Length);

            // Fetching Spawn Position
            Vector2 spawnPosition = new Vector2(0f, 0f);
            playerActorController = new PlayerActorController(
                actorConfig, spawnPosition, actorIndex,
                soundService, projectileService, this);
        }
        private void CreateEnemy(Vector2 _spawnPosition)
        {
            // Fetching Random Index
            int actorIndex = Random.Range(0, actorConfig.enemyData.Length);

            // Get spawn position from SpawnController
            var enemyActorController = new EnemyActorController(actorConfig, _spawnPosition, actorIndex,
                soundService, projectileService, this
            );
            enemyActorControllers.Add(enemyActorController);
        }

        public void Update()
        {
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

        // Getters
        public ActorController GetPlayerActorController() => playerActorController;
        public List<ActorController> GetEnemyActorControllers() => enemyActorControllers;
    }
}