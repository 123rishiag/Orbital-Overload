using ServiceLocator.Actor;
using ServiceLocator.Event;
using UnityEngine;

namespace ServiceLocator.Projectile
{
    public class HomingBulletProjectileController : ProjectileController
    {
        private ActorView nearestEnemy; // Target enemy for homing projectiles
        public HomingBulletProjectileController(ProjectileData _projectileData, ProjectileView _projectilePrefab,
            Transform _projectileParentPanel, ActorType _projectileOwnerActor, float _shootSpeed,
            Color _projectileColor, Transform _shootPoint,
            EventService _eventService, ActorService _actorService) :

            base(_projectileData, _projectilePrefab,
                _projectileParentPanel, _projectileOwnerActor, _shootSpeed,
                 _projectileColor, _shootPoint,
            _eventService, _actorService)
        { }

        public override void Update()
        {
            FindNearestEnemy(); // Find the nearest enemy for homing
        }

        public override void FixedUpdate()
        {
            Homing(); // Homing logic in FixedUpdate for physics
        }

        private void FindNearestEnemy()
        {
            if (projectileModel.ProjectileType == ProjectileType.Homing_Bullet)
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
                    nearestEnemy = nearestActor.GetComponent<ActorView>();
                }
            }
        }

        private void Homing()
        {
            if (projectileModel.ProjectileType == ProjectileType.Homing_Bullet && nearestEnemy != null)
            {
                Vector2 enemyDirection = ((Vector2)nearestEnemy.transform.position - projectileView.rigidBody.position).normalized;
                projectileView.rigidBody.velocity =
                    Vector2.Lerp(projectileView.rigidBody.velocity, enemyDirection * projectileModel.ShootSpeed, Time.fixedDeltaTime);
            }
        }
    }
}