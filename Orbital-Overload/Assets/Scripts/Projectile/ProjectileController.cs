using ServiceLocator.Actor;
using ServiceLocator.Sound;
using UnityEngine;

namespace ServiceLocator.Projectile
{
    public abstract class ProjectileController
    {
        // Private Variables
        protected ProjectileModel projectileModel;
        protected ProjectileView projectileView;

        // Private Services
        protected SoundService soundService;
        protected ActorService actorService;

        public ProjectileController(ProjectileConfig _projectileConfig,
            ActorType _projectileOwnerActor, float _shootSpeed, Transform _shootPoint, int _projectileIndex,
            SoundService _soundService, ActorService _actorService)
        {
            projectileModel =
                new ProjectileModel(_projectileConfig.projectileData[_projectileIndex], _projectileOwnerActor, _shootSpeed);
            projectileView = Object.Instantiate(_projectileConfig.projectilePrefab, _shootPoint.position, _shootPoint.rotation).
                GetComponent<ProjectileView>();
            projectileView.Init(this);

            // Setting Services
            soundService = _soundService;
            actorService = _actorService;

            // Setting Elements
            ShootProjectile(_shootPoint, _shootSpeed); // Shoot the projectile
        }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        public void ShootProjectile(Transform _shootPoint, float _projectileSpeed)
        {
            projectileView.rigidBody.velocity = _shootPoint.up * _projectileSpeed * Time.fixedDeltaTime; // Set projectile velocity
            soundService.PlaySoundEffect(SoundType.ProjectileShoot);
        }

        // Getters
        public ProjectileModel GetProjectileModel() => projectileModel;
        public ProjectileView GetProjectileView() => projectileView;
    }
}