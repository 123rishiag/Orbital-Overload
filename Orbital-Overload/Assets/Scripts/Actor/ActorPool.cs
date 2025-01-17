using ServiceLocator.Control;
using ServiceLocator.Projectile;
using ServiceLocator.Sound;
using ServiceLocator.UI;
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
        private SoundService soundService;
        private UIService uiService;
        private InputService inputService;
        private ProjectileService projectileService;
        private ActorService actorService;

        public ActorPool(ActorConfig _actorConfig, Transform _actorParentPanel,
            SoundService _soundService, UIService _uiService, InputService _inputService,
            ProjectileService _projectileService, ActorService _actorService)
        {
            // Setting Variables
            actorConfig = _actorConfig;
            actorParentPanel = _actorParentPanel;

            // Setting Services
            soundService = _soundService;
            uiService = _uiService;
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
                        soundService, uiService, inputService, projectileService, actorService
                        );
                case ActorType.Fast_Enemy:
                    return new EnemyActorController(actorConfig.enemyData[actorIndex], actorConfig.actorPrefab,
                        actorParentPanel, spawnPosition,
                        actorConfig.enemyAwayFromPlayerMinDistance,
                        soundService, uiService, inputService, projectileService, actorService
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