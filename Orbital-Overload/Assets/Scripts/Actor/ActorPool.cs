using ServiceLocator.Control;
using ServiceLocator.Projectile;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Utility;
using UnityEngine;

namespace ServiceLocator.Actor
{
    public class ActorPool : GenericObjectPool<ActorController>
    {
        // Private Variables
        private ActorConfig actorConfig;
        private Transform actorParentPanel;

        private Vector2 spawnPosition;

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

        public ActorController GetActor(Vector2 _spawnPosition)
        {
            // Setting Variables
            spawnPosition = _spawnPosition;

            // Fetching Item
            var item = GetItem<ActorController>();

            // Fetching Index
            int actorIndex = GetRandomActorIndex();

            // Resetting Item Properties
            item.Reset(actorConfig.enemyData[actorIndex], spawnPosition);

            return item;
        }

        protected override ActorController CreateItem<T>()
        {
            // Fetching Index
            int actorIndex = GetRandomActorIndex();

            // Creating Controller
            return new EnemyActorController(actorConfig.enemyData[actorIndex], actorConfig.actorPrefab,
                actorParentPanel, spawnPosition,
                actorConfig.enemyAwayFromPlayerMinDistance,
                soundService, uiService, inputService, projectileService, actorService
            );
        }

        // Getters
        private int GetRandomActorIndex()
        {
            // Fetching Random Index
            return Random.Range(0, actorConfig.enemyData.Length);
        }
    }
}