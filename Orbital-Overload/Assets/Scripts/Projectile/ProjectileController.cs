using ServiceLocator.Actor;
using ServiceLocator.Sound;
using UnityEngine;

namespace ServiceLocator.Projectile
{
    public class ProjectileController
    {
        // Private Variables
        protected ProjectileModel projectileModel;
        protected ProjectileView projectileView;

        // Private Services
        protected SoundService soundService;
        protected ActorService actorService;

        public ProjectileController(ProjectileData _projectileData,
            ProjectileView _projectilePrefab, ActorType _projectileOwnerActor, float _shootSpeed, Transform _shootPoint,
            SoundService _soundService, ActorService _actorService)
        {
            projectileModel =
                new ProjectileModel(_projectileData, _projectileOwnerActor, _shootSpeed);
            projectileView = Object.Instantiate(_projectilePrefab, _shootPoint.position, _shootPoint.rotation).
                GetComponent<ProjectileView>();
            projectileView.Init(this);

            // Setting Services
            soundService = _soundService;
            actorService = _actorService;

            // Setting Elements
            ShootProjectile(_shootPoint, _shootSpeed); // Shoot the projectile
            Debug.Log("New");
        }

        public void Reset(ProjectileData _projectileData, ActorType _projectileOwnerActor, float _shootSpeed,
            Transform _shootPoint)
        {
            projectileModel.Reset(_projectileData, _projectileOwnerActor, _shootSpeed);
            projectileView.Reset();
            projectileView.SetPosition(_shootPoint.position);
            projectileView.ShowView();
            ShootProjectile(_shootPoint, _shootSpeed);
            Debug.Log("Old");
        }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        private void ShootProjectile(Transform _shootPoint, float _shootSpeed)
        {
            projectileView.rigidBody.velocity = _shootPoint.up * _shootSpeed * Time.fixedDeltaTime; // Set projectile velocity
            soundService.PlaySoundEffect(SoundType.ProjectileShoot);
        }

        public bool IsActive()
        {
            if (projectileView == null) return false;
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(projectileView.transform.position);
            if (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1)
            {
                return false;
            }
            return true;
        }

        // Getters
        public ProjectileModel GetProjectileModel() => projectileModel;
        public ProjectileView GetProjectileView() => projectileView;
    }
}