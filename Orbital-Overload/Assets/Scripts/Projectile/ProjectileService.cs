using ServiceLocator.Actor;
using ServiceLocator.Sound;
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

        public void Shoot(ActorType _projectileOwnerActor, float _shootSpeed, bool _isHoming, Transform _shootPoint)
        {
            ProjectileController projectileController =
                new ProjectileController(projectileConfig, _projectileOwnerActor, _shootSpeed, _isHoming, _shootPoint,
                soundService, actorService);
            projectiles.Add(projectileController);
        }
    }
}