using ServiceLocator.Actor;
using ServiceLocator.Sound;
using UnityEngine;

namespace ServiceLocator.Projectile
{
    public class ProjectileController
    {
        // Private Variables
        private ProjectileModel projectileModel;
        private ProjectileView projectileView;
        public ActorView enemy; // Target enemy for homing projectiles

        // Private Services
        private SoundService soundService;
        private ActorService actorService;

        public ProjectileController(ProjectileConfig _projectileConfig,
            ActorType _projectileOwnerActor, float _shootSpeed, bool _isHoming, Transform _shootPoint,
            SoundService _soundService, ActorService _actorService)
        {
            projectileModel = new ProjectileModel(_projectileConfig.projectileData, _projectileOwnerActor, _isHoming);
            projectileView = Object.Instantiate(_projectileConfig.projectilePrefab, _shootPoint.position, _shootPoint.rotation).
                GetComponent<ProjectileView>();
            projectileView.Init(this);

            // Setting Services
            soundService = _soundService;
            actorService = _actorService;

            // Setting Elements
            ShootProjectile(_shootPoint, _shootSpeed); // Shoot the projectile
        }

        public void Update()
        {
            FindNearestEnemy(); // Find the nearest enemy for homing
        }

        public void FixedUpdate()
        {
            Homing(); // Homing logic in FixedUpdate for physics
        }

        private void FindNearestEnemy()
        {
            if (projectileModel.IsHoming && enemy == null)
            {
                ActorView nearestActor = null;
                float minDistance = Mathf.Infinity;
                Vector2 currentPosition = projectileView.transform.position;

                // Find the nearest enemy
                foreach (var actorController in actorService.GetEnemyActorControllers())
                {
                    // Avoid Hitting Player
                    if (actorController.GetActorModel().ActorType == ActorType.Player) continue;

                    // Avoid Dead Enemies
                    if (!actorController.IsAlive()) return;

                    // Fetching Distance from enemies
                    float distance = Vector2.Distance(actorController.GetActorView().transform.position, currentPosition);
                    if (distance < minDistance)
                    {
                        nearestActor = actorController.GetActorView();
                        minDistance = distance;
                    }
                }
                if (nearestActor != null)
                {
                    enemy = nearestActor.GetComponent<ActorView>();
                }
            }
        }

        private void Homing()
        {
            if (projectileModel.IsHoming && enemy != null)
            {
                Vector2 enemyDirection = ((Vector2)enemy.transform.position - projectileView.rigidBody.position).normalized;
                projectileView.rigidBody.velocity =
                    Vector2.Lerp(projectileView.rigidBody.velocity, enemyDirection * projectileModel.HomingSpeed, Time.fixedDeltaTime);
            }
        }

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