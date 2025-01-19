using ServiceLocator.Control;
using ServiceLocator.Event;
using ServiceLocator.Projectile;
using ServiceLocator.Spawn;
using ServiceLocator.VFX;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ServiceLocator.Actor
{
    public class ActorService
    {
        // Private Variables
        private ActorConfig actorConfig;
        private Transform actorParentPanel;

        private ActorPool actorPool;
        private ActorController playerActorController;

        // Private Services
        private EventService eventService;
        private InputService inputService;
        private SpawnService spawnService;
        private ProjectileService projectileService;

        public ActorService(ActorConfig _actorConfig, Transform _actorParentPanel)
        {
            // Setting Variables
            actorConfig = _actorConfig;
            actorParentPanel = _actorParentPanel;
        }

        public void Init(EventService _eventService, InputService _inputService,
            SpawnService _spawnService, ProjectileService _projectileService)
        {
            // Setting Services
            eventService = _eventService;
            inputService = _inputService;
            spawnService = _spawnService;
            projectileService = _projectileService;

            // Setting Elements
            CreatePlayer();

            // Creating Object Pool for enemies
            actorPool = new ActorPool(actorConfig, actorParentPanel,
                eventService, inputService,
                projectileService, this);

            // Creating spawn controller for enemies
            spawnService.CreateSpawnController(actorConfig.enemySpawnInterval, actorConfig.enemySpawnRadius,
                actorConfig.enemyAwayFromPlayerSpawnDistance, CreateEnemy);
        }

        private void CreatePlayer()
        {
            // Fetching Spawn Position
            Vector2 spawnPosition = new Vector2(0f, 0f);
            playerActorController = new PlayerActorController(
                actorConfig.playerData, actorConfig.actorPrefab,
                actorParentPanel, spawnPosition,
                actorConfig.playerCasualMoveSpeed,
                eventService, inputService, projectileService, this);
        }
        private void CreateEnemy(Vector2 _spawnPosition)
        {
            // Fetching Random Index
            int enemyIndex = Random.Range(0, actorConfig.enemyData.Length);
            ActorType actorType = actorConfig.enemyData[enemyIndex].actorType;

            // Fetching Actor
            switch (actorType)
            {
                case ActorType.Normal_Enemy:
                    actorPool.GetActor<EnemyActorController>(_spawnPosition, actorType);
                    break;
                case ActorType.Fast_Enemy:
                    actorPool.GetActor<EnemyActorController>(_spawnPosition, actorType);
                    break;
                default:
                    Debug.LogWarning($"Unhandled ActorType: {actorType}");
                    break;
            }
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

        public void Reset()
        {
            // Disabling All Actors
            for (int i = actorPool.pooledItems.Count - 1; i >= 0; i--)
            {
                // Skipping if the pooled item's isUsed is false
                if (!actorPool.pooledItems[i].isUsed)
                {
                    continue;
                }

                var actorController = actorPool.pooledItems[i].Item;
                ReturnActorToPool(actorController);
            }

            // Resetting Player
            Vector2 spawnPosition = new Vector2(0f, 0f);
            playerActorController.Reset(actorConfig.playerData, spawnPosition);
        }

        private void ReturnActorToPool(ActorController _actorToReturn)
        {
            eventService.OnCreateVFXEvent.Invoke(VFXType.Splatter, _actorToReturn.GetActorView().GetTransform(),
                _actorToReturn.GetActorModel().ActorColor);
            _actorToReturn.GetActorView().HideView();
            actorPool.ReturnItem(_actorToReturn);
        }

        // Getters
        public ActorController GetPlayerActorController() => playerActorController;
        public List<ActorController> GetEnemyActorControllers() =>
            actorPool.pooledItems.Where(item => item.isUsed).Select(item => item.Item).ToList();
    }
}