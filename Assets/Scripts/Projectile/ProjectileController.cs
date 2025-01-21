using ServiceLocator.Actor;
using ServiceLocator.Event;
using ServiceLocator.Sound;
using ServiceLocator.VFX;
using UnityEngine;

namespace ServiceLocator.Projectile
{
    public class ProjectileController
    {
        // Private Variables
        protected ProjectileModel projectileModel;
        protected ProjectileView projectileView;

        // Private Services
        protected EventService eventService;
        protected ActorService actorService;

        public ProjectileController(ProjectileData _projectileData, ProjectileView _projectilePrefab,
            Transform _projectileParentPanel, ActorType _projectileOwnerActor, float _shootSpeed,
            Color _projectileColor, Transform _shootPoint,
            EventService _eventService, ActorService _actorService)
        {
            projectileModel =
                new ProjectileModel(_projectileData, _projectileOwnerActor, _shootSpeed, _projectileColor);
            projectileView = Object.Instantiate(_projectilePrefab, _shootPoint.position, _shootPoint.rotation,
                _projectileParentPanel).GetComponent<ProjectileView>();
            projectileView.Init(this);

            // Setting Services
            eventService = _eventService;
            actorService = _actorService;

            // Setting Elements
            ShootProjectile(_shootPoint, _shootSpeed); // Shoot the projectile
        }

        public void Reset(ProjectileData _projectileData, ActorType _projectileOwnerActor, float _shootSpeed,
            Color _projectileColor, Transform _shootPoint)
        {
            projectileModel.Reset(_projectileData, _projectileOwnerActor, _shootSpeed, _projectileColor);
            projectileView.Reset();
            projectileView.SetPosition(_shootPoint.position);
            projectileView.ShowView();
            ShootProjectile(_shootPoint, _shootSpeed);
        }

        public virtual void Update() { }

        public virtual void FixedUpdate() { }

        private void ShootProjectile(Transform _shootPoint, float _shootSpeed)
        {
            projectileView.rigidBody.velocity = _shootPoint.up * _shootSpeed * Time.fixedDeltaTime; // Set projectile velocity
            eventService.OnPlaySoundEvent.Invoke(SoundType.ProjectileShoot);
        }

        public void PlayVFX()
        {
            eventService.OnCreateVFXEvent.Invoke(VFXType.Splatter, projectileView.transform,
                projectileModel.ProjectileColor);
        }

        public bool IsActive()
        {
            if (!projectileView.gameObject.activeInHierarchy) return false;
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