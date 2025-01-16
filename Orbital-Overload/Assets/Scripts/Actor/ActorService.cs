using ServiceLocator.Control;
using ServiceLocator.Projectile;
using ServiceLocator.Sound;
using ServiceLocator.Spawn;
using ServiceLocator.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ServiceLocator.Actor
{
    public class ActorService
    {
        // Private Variables
        private ActorConfig actorConfig;
        private ActorPool actorPool;
        private ActorController playerActorController;

        // Private Services
        private SoundService soundService;
        private UIService uiService;
        private InputService inputService;
        private SpawnService spawnService;
        private ProjectileService projectileService;

        public ActorService(ActorConfig _actorConfig)
        {
            // Setting Variables
            actorConfig = _actorConfig;
        }

        public void Init(SoundService _soundService, UIService _uiService, InputService _inputService,
            SpawnService _spawnService, ProjectileService _projectileService)
        {
            // Setting Services
            soundService = _soundService;
            uiService = _uiService;
            inputService = _inputService;
            spawnService = _spawnService;
            projectileService = _projectileService;

            // Setting Elements
            CreatePlayer();

            // Creating Object Pool for enemies
            actorPool = new ActorPool(actorConfig,
                _soundService, _uiService, _inputService,
            _projectileService, this);

            // Creating spawn controller for enemies
            spawnService.CreateSpawnController(actorConfig.enemySpawnInterval, actorConfig.enemySpawnRadius,
                actorConfig.enemyAwayFromPlayerSpawnDistance, CreateEnemy);
        }

        private void CreatePlayer()
        {
            // Fetching Random Index
            int actorIndex = Random.Range(0, actorConfig.enemyData.Length);

            // Fetching Spawn Position
            Vector2 spawnPosition = new Vector2(0f, 0f);
            playerActorController = new PlayerActorController(
                actorConfig.playerData, actorConfig.actorPrefab, spawnPosition,
                actorConfig.playerCasualMoveSpeed,
                soundService, uiService, inputService, projectileService, this);
        }
        private void CreateEnemy(Vector2 _spawnPosition)
        {
            actorPool.GetActor(_spawnPosition);
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
            for (int i = actorPool.pooledItems.Count - 1; i >= 0; i--)
            {
                // Skipping if the pooled item's isUsed is false
                if (!actorPool.pooledItems[i].isUsed)
                {
                    continue;
                }

                var actorController = actorPool.pooledItems[i].Item;
                actorController.Update();

                if (!actorController.IsAlive())
                {
                    ReturnActorToPool(actorController);
                }
            }
        }
        private void ProcessActorFixedUpdate()
        {
            // For Player
            playerActorController.FixedUpdate();

            // For Enemies
            for (int i = actorPool.pooledItems.Count - 1; i >= 0; i--)
            {
                // Skipping if the pooled item's isUsed is false
                if (!actorPool.pooledItems[i].isUsed)
                {
                    continue;
                }

                var actorController = actorPool.pooledItems[i].Item;
                actorController.FixedUpdate();
            }
        }

        private void ReturnActorToPool(ActorController _actorToReturn)
        {
            _actorToReturn.GetActorView().HideView();
            actorPool.ReturnItem(_actorToReturn);
        }

        // Getters
        public ActorController GetPlayerActorController() => playerActorController;
        public List<ActorController> GetEnemyActorControllers() =>
            actorPool.pooledItems.Where(item => item.isUsed).Select(item => item.Item).ToList();
    }
}