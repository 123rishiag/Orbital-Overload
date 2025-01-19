using ServiceLocator.Actor;
using ServiceLocator.Event;
using ServiceLocator.VFX;
using UnityEngine;

namespace ServiceLocator.Projectile
{
    public class ProjectileService
    {
        // Private Variables
        private ProjectileConfig projectileConfig;
        private Transform projectileParentPanel;
        private ProjectilePool projectilePool;

        public ProjectileService(ProjectileConfig _projectileConfig, Transform _projectileParentPanel)
        {
            // Setting Variables
            projectileConfig = _projectileConfig;
            projectileParentPanel = _projectileParentPanel;
        }

        public void Init(EventService _eventService, ActorService _actorService)
        {
            // Creating Object Pool for projectiles
            projectilePool = new ProjectilePool(projectileConfig, projectileParentPanel, _eventService, _actorService);
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

        public void Reset()
        {
            // Disabling All Projectiles
            for (int i = projectilePool.pooledItems.Count - 1; i >= 0; i--)
            {
                var projectileController = projectilePool.pooledItems[i].Item;
                ReturnProjectileToPool(projectileController);
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