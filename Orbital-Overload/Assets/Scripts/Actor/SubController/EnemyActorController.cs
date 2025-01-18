using ServiceLocator.Control;
using ServiceLocator.Event;
using ServiceLocator.Projectile;
using UnityEngine;

namespace ServiceLocator.Actor
{
    public class EnemyActorController : ActorController
    {
        // Private Variables
        public float enemyAwayFromPlayerMinDistance; // Minimum distance from player enemy should maintain

        public EnemyActorController(ActorData _actorData, ActorView _actorPrefab,
            Transform _actorParentPanel, Vector2 _spawnPosition,
            float _enemyAwayFromPlayerMinDistance,
            EventService _eventService, InputService _inputService,
            ProjectileService _projectileService, ActorService _actorService) :

            base(_actorData, _actorPrefab,
                _actorParentPanel, _spawnPosition,
                _eventService, _inputService,
                _projectileService, _actorService)
        {
            // Setting Variables
            isShooting = true;
            enemyAwayFromPlayerMinDistance = _enemyAwayFromPlayerMinDistance;
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

        public override void AddScore(int _score)
        {
            actorService.GetPlayerActorController().GetActorModel().CurrentScore += _score; // Increase score
            actorService.GetPlayerActorController().UpdateScoreUI();
        }
    }
}