using ServiceLocator.Actor;
using ServiceLocator.Sound;
using ServiceLocator.Utility;
using System;
using UnityEngine;

namespace ServiceLocator.Projectile
{
    public class ProjectilePool : GenericObjectPool<ProjectileController>
    {
        // Private Variables
        private ProjectileConfig projectileConfig;
        private ActorType projectileOwnerActor;
        private float shootSpeed;
        private Transform shootPoint;
        private ProjectileType projectileType;

        // Private Services
        private SoundService soundService;
        private ActorService actorService;

        public ProjectilePool(ProjectileConfig _projectileConfig, SoundService _soundService, ActorService _actorService)
        {
            // Setting Variables
            projectileConfig = _projectileConfig;

            // Setting Services
            soundService = _soundService;
            actorService = _actorService;
        }

        public ProjectileController GetProjectile(ActorType _projectileOwnerActor, float _shootSpeed,
            Transform _shootPoint, ProjectileType _projectileType)
        {
            // Setting Variables
            projectileOwnerActor = _projectileOwnerActor;
            shootSpeed = _shootSpeed;
            shootPoint = _shootPoint;
            projectileType = _projectileType;

            // Fetching Item
            var item = GetItem<ProjectileController>();

            // Fetching Index
            int projectileIndex = GetProjectileIndex();

            // Resetting Item Properties
            item.Reset(projectileConfig.projectileData[projectileIndex], projectileOwnerActor, shootSpeed,
            shootPoint);

            return item;
        }

        protected override ProjectileController CreateItem<T>()
        {
            // Fetching Index
            int projectileIndex = GetProjectileIndex();

            // Creating Controller
            switch (projectileType)
            {
                case ProjectileType.Normal_Bullet:
                    return new ProjectileController(projectileConfig.projectileData[projectileIndex],
                        projectileConfig.projectilePrefab, projectileOwnerActor, shootSpeed, shootPoint,
                        soundService, actorService);
                case ProjectileType.Homing_Bullet:
                    return new HomingBulletProjectileController(projectileConfig.projectileData[projectileIndex],
                        projectileConfig.projectilePrefab, projectileOwnerActor, shootSpeed, shootPoint,
                        soundService, actorService);
                default:
                    Debug.LogWarning($"Unhandled ProjectileType: {projectileType}");
                    return null;
            }
        }

        // Getters
        private int GetProjectileIndex()
        {
            // Fetching Index
            return Array.FindIndex(projectileConfig.projectileData, data => data.projectileType == projectileType);
        }
    }
}