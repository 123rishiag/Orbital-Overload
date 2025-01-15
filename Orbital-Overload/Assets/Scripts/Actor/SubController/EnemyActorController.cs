using ServiceLocator.Projectile;
using ServiceLocator.Sound;
using UnityEngine;

namespace ServiceLocator.Actor
{
    public class EnemyActorController : ActorController
    {
        // Private Variables
        public float enemyAwayFromPlayerMinDistance; // Minimum distance from player enemy should maintain

        public EnemyActorController(ActorConfig _actorConfig, Vector2 _spawnPosition, int _actorIndex,
            SoundService _soundService, ProjectileService _projectileService, ActorService _actorService) :
            base(_actorConfig.enemyData[_actorIndex], _actorConfig.actorPrefab, _spawnPosition,
                _soundService, _projectileService, _actorService)
        {
            // Setting Variables
            isShooting = true;
            enemyAwayFromPlayerMinDistance = _actorConfig.enemyAwayFromPlayerMinDistance;
        }

        protected override void MovementInput()
        {
            Vector2 playerPosition = actorService.GetPlayerActorController().GetActorView().GetPosition();
            float distanceToPlayer = Vector2.Distance(actorView.transform.position, playerPosition);
            if (distanceToPlayer > enemyAwayFromPlayerMinDistance)
            {
                Vector2 direction = (playerPosition - (Vector2)actorView.transform.position).normalized;
                moveX = direction.x;
                moveY = direction.y;
            }
            else
            {
                moveX = 0.0f;
                moveY = 0.0f;
            }
        }
        protected override void ShootInput() { }
        protected override void RotateInput()
        {
            Vector2 playerPosition = actorService.GetPlayerActorController().GetActorView().GetPosition();
            mouseDirection = (playerPosition - (Vector2)actorView.transform.position).normalized;
        }
    }
}