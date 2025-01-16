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

        // Private Services
        private SoundService soundService;
        private UIService uiService;
        private InputService inputService;
        private ProjectileService projectileService;
        private ActorService actorService;

        public ActorPool(ActorConfig _actorConfig, SoundService _soundService, UIService _uiService, InputService _inputService,
            ProjectileService _projectileService, ActorService _actorService)
        {
            // Setting Variables
            actorConfig = _actorConfig;

            // Setting Services
            soundService = _soundService;
            uiService = _uiService;
            inputService = _inputService;
            projectileService = _projectileService;
            actorService = _actorService;
        }

        public ActorController GetActor(Vector2 _spawnPosition)
        {
            var item = GetItem(_spawnPosition);

            // Fetching Random Index
            int actorIndex = GetRandomActorIndex();

            // Resetting Item Properties
            item.Reset(actorConfig.enemyData[actorIndex], _spawnPosition);

            return item;
        }

        protected override ActorController CreateItem(Vector2 _spawnPosition)
        {
            // Fetching Random Index
            int actorIndex = GetRandomActorIndex();

            // Creating Controller
            return new EnemyActorController(actorConfig.enemyData[actorIndex], actorConfig.actorPrefab, _spawnPosition,
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