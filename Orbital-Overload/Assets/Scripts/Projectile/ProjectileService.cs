using ServiceLocator.Actor;
using ServiceLocator.Sound;
using UnityEngine;

namespace ServiceLocator.Projectile
{
    public class ProjectileService
    {
        // Private Variables
        private ProjectileConfig projectileConfig;
        private Transform projectileParentPanel;
        private ProjectilePool projectilePool;

        // Private Services
        private SoundService soundService;
        private ActorService actorService;

        public ProjectileService(ProjectileConfig _projectileConfig, Transform _projectileParentPanel)
        {
            // Setting Variables
            projectileConfig = _projectileConfig;
            projectileParentPanel = _projectileParentPanel;
        }

        public void Init(SoundService _soundService, ActorService _actorService)
        {
            // Setting Services
            soundService = _soundService;
            actorService = _actorService;

            // Setting Elements
            projectilePool = new ProjectilePool(projectileConfig, projectileParentPanel, soundService, actorService);
        }

        public void Update()
        {
            ProcessProjectileUpdate();
        }
        public void FixedUpdate()
        {
            ProcessProjectileFixedUpdate();
        }

        private void ProcessProjectileUpdate()
        {
            for (int i = projectilePool.pooledItems.Count - 1; i >= 0; i--)
            {
                // Skipping if the pooled item's isUsed is false
                if (!projectilePool.pooledItems[i].isUsed)
                {
                    continue;
                }

                var projectileController = projectilePool.pooledItems[i].Item;
                projectileController.Update();

                if (!projectileController.IsActive())
                {
                    ReturnProjectileToPool(projectileController);
                }
            }
        }
        private void ProcessProjectileFixedUpdate()
        {
            for (int i = projectilePool.pooledItems.Count - 1; i >= 0; i--)
            {
                // Skipping if the pooled item's isUsed is false
                if (!projectilePool.pooledItems[i].isUsed)
                {
                    continue;
                }

                var projectileController = projectilePool.pooledItems[i].Item;
                projectileController.FixedUpdate();
            }
        }

        public void Shoot(
            ActorType _projectileOwnerActor, float _shootSpeed, Transform _shootPoint, ProjectileType _projectileType)
        {
            // Fetching Projectile
            switch (_projectileType)
            {
                case ProjectileType.Normal_Bullet:
                    projectilePool.GetProjectile<ProjectileController>(_projectileOwnerActor, _shootSpeed,
            _shootPoint, _projectileType);
                    break;
                case ProjectileType.Homing_Bullet:
                    projectilePool.GetProjectile<HomingBulletProjectileController>(_projectileOwnerActor, _shootSpeed,
            _shootPoint, _projectileType);
                    break;
                default:
                    Debug.LogWarning($"Unhandled ProjectileType: {_projectileType}");
                    break;
            }
        }

        private void ReturnProjectileToPool(ProjectileController _projectileToReturn)
        {
            _projectileToReturn.GetProjectileView().HideView();
            projectilePool.ReturnItem(_projectileToReturn);
        }
    }
}