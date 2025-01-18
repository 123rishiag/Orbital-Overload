using ServiceLocator.Control;
using ServiceLocator.Event;
using ServiceLocator.Projectile;
using ServiceLocator.Utility;
using System;
using UnityEngine;

namespace ServiceLocator.Actor
{
    public class ActorPool : GenericObjectPool<ActorController>
    {
        // Private Variables
        private ActorConfig actorConfig;
        private Transform actorParentPanel;

        private Vector2 spawnPosition;
        private ActorType actorType;

        // Private Services
        private EventService eventService;
        private InputService inputService;
        private ProjectileService projectileService;
        private ActorService actorService;

        public ActorPool(ActorConfig _actorConfig, Transform _actorParentPanel,
            EventService _eventService, InputService _inputService,
            ProjectileService _projectileService, ActorService _actorService)
        {
            // Setting Variables
            actorConfig = _actorConfig;
            actorParentPanel = _actorParentPanel;

            // Setting Services
            eventService = _eventService;
            inputService = _inputService;
            projectileService = _projectileService;
            actorService = _actorService;
        }

        public ActorController GetActor<T>(Vector2 _spawnPosition, ActorType _actorType) where T : ActorController
        {
            // Setting Variables
            spawnPosition = _spawnPosition;
            actorType = _actorType;

            // Fetching Item
            var item = GetItem<T>();

            // Fetching Index
            int actorIndex = GetActorIndex();

            // Resetting Item Properties
            item.Reset(actorConfig.enemyData[actorIndex], spawnPosition);

            return item;
        }

        protected override ActorController CreateItem<T>()
        {
            // Fetching Index
            int actorIndex = GetActorIndex();

            // Creating Controller
            switch (actorType)
            {
                case ActorType.Normal_Enemy:
                    return new EnemyActorController(actorConfig.enemyData[actorIndex], actorConfig.actorPrefab,
                        actorParentPanel, spawnPosition,
                        actorConfig.enemyAwayFromPlayerMinDistance,
                        eventService, inputService,
                        projectileService, actorService
                        );
                case ActorType.Fast_Enemy:
                    return new EnemyActorController(actorConfig.enemyData[actorIndex], actorConfig.actorPrefab,
                        actorParentPanel, spawnPosition,
                        actorConfig.enemyAwayFromPlayerMinDistance,
                        eventService, inputService,
                        projectileService, actorService
                        );
                default:
                    Debug.LogWarning($"Unhandled ActorType: {actorType}");
                    return null;
            }
        }

        // Getters
        private int GetActorIndex()
        {
            // Fetching Index
            return Array.FindIndex(actorConfig.enemyData, data => data.actorType == actorType);
        }
    }
}