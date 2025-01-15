using ServiceLocator.Actor;
using ServiceLocator.Sound;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Projectile
{
    public class ProjectileService
    {
        // Private Variables
        private ProjectileConfig projectileConfig;
        private List<ProjectileController> projectiles;

        // Private Services
        private SoundService soundService;
        private ActorService actorService;

        public ProjectileService(ProjectileConfig _projectileConfig)
        {
            // Setting Variables
            projectileConfig = _projectileConfig;
            projectiles = new List<ProjectileController>();
        }

        public void Init(SoundService _soundService, ActorService _actorService)
        {
            // Setting Services
            soundService = _soundService;
            actorService = _actorService;
        }

        public void Update()
        {
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                ProjectileController projectile = projectiles[i];
                if (projectile.GetProjectileView() != null)
                {
                    projectile.Update();
                }
                else
                {
                    projectiles.RemoveAt(i); // Safely remove without affecting iteration
                }
            }
        }

        public void FixedUpdate()
        {
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                ProjectileController projectile = projectiles[i];
                if (projectile.GetProjectileView() != null)
                {
                    projectile.FixedUpdate();
                }
            }
        }

        public void Shoot(
            ActorType _projectileOwnerActor, float _shootSpeed, Transform _shootPoint, ProjectileType _projectileType)
        {
            // Fetching Index of Projectile Type
            int projectileIndex = Array.FindIndex(projectileConfig.projectileData, data => data.projectileType == _projectileType);

            // Created the controller
            ProjectileController projectileController = CreateProjectileController(
                _projectileType, _projectileOwnerActor, _shootSpeed, _shootPoint, projectileIndex);

            // Add the created controller to the list if valid
            if (projectileController != null)
            {
                projectiles.Add(projectileController);
            }
        }

        private ProjectileController CreateProjectileController(
            ProjectileType _projectileType,
            ActorType _projectileOwnerActor, float _shootSpeed, Transform _shootPoint, int _projectileIndex)
        {
            switch (_projectileType)
            {
                case ProjectileType.Normal_Bullet:
                    return new ProjectileController(projectileConfig,
                        _projectileOwnerActor, _shootSpeed, _shootPoint, _projectileIndex,
                        soundService, actorService);
                case ProjectileType.Homing_Bullet:
                    return new HomingBulletProjectileController(projectileConfig,
                        _projectileOwnerActor, _shootSpeed, _shootPoint, _projectileIndex,
                        soundService, actorService);
                default:
                    Debug.LogWarning($"Unhandled ProjectileType: {_projectileType}");
                    return null;
            }
        }
    }
}