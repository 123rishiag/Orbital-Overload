using ServiceLocator.Actor;
using ServiceLocator.Event;
using ServiceLocator.Utility;
using System;
using UnityEngine;

namespace ServiceLocator.Projectile
{
    public class ProjectilePool : GenericObjectPool<ProjectileController>
    {
        // Private Variables
        private ProjectileConfig projectileConfig;
        private Transform projectileParentPanel;

        private ActorType projectileOwnerActor;
        private float shootSpeed;
        private Transform shootPoint;
        private ProjectileType projectileType;
        private Color projectileColor;

        // Private Services
        private EventService eventService;
        private ActorService actorService;

        public ProjectilePool(ProjectileConfig _projectileConfig, Transform _projectileParentPanel,
            EventService _eventService, ActorService _actorService)
        {
            // Setting Variables
            projectileConfig = _projectileConfig;
            projectileParentPanel = _projectileParentPanel;

            // Setting Services
            eventService = _eventService;
            actorService = _actorService;
        }

        public ProjectileController GetProjectile<T>(ActorType _projectileOwnerActor, float _shootSpeed,
            Transform _shootPoint, ProjectileType _projectileType, Color _projectileColor) where T : ProjectileController
        {
            // Setting Variables
            projectileOwnerActor = _projectileOwnerActor;
            shootSpeed = _shootSpeed;
            shootPoint = _shootPoint;
            projectileType = _projectileType;
            projectileColor = _projectileColor;

            // Fetching Item
            var item = GetItem<T>();

            // Fetching Index
            int projectileIndex = GetProjectileIndex();

            // Resetting Item Properties
            item.Reset(projectileConfig.projectileData[projectileIndex], projectileOwnerActor, shootSpeed,
            projectileColor, shootPoint);

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
                        projectileConfig.projectilePrefab, projectileParentPanel, projectileOwnerActor, shootSpeed,
                        projectileColor, shootPoint,
                        eventService, actorService);
                case ProjectileType.Homing_Bullet:
                    return new HomingBulletProjectileController(projectileConfig.projectileData[projectileIndex],
                        projectileConfig.projectilePrefab, projectileParentPanel, projectileOwnerActor, shootSpeed,
                        projectileColor, shootPoint,
                        eventService, actorService);
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