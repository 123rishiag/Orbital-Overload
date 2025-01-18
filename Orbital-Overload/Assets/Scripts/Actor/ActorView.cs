using ServiceLocator.Projectile;
using UnityEngine;

namespace ServiceLocator.Actor
{
    public class ActorView : MonoBehaviour
    {
        [SerializeField] public Transform shootPoint; // Point from where projectiles are shot
        [SerializeField] public SpriteRenderer actorSprite; // Actor Sprite
        [SerializeField] public SpriteRenderer actorShooterSprite; // Actor's Shooter Sprite
        [SerializeField] private Animator actorAnimator; // Actor's Animator
        [SerializeField] private Animator shooterAnimator; // Actor's Shooter Animator

        [HideInInspector]
        public ActorController actorController;

        // Private Variables
        private static readonly int IDLE_HASH = Animator.StringToHash("Idle");
        private static readonly int SHOOT_HASH = Animator.StringToHash("Shoot");

        public void Init(ActorController _actorController)
        {
            // Setting Variables
            actorController = _actorController;
            Reset();
        }

        public void Reset()
        {
            actorSprite.color = actorController.GetActorModel().ActorColor;
            actorShooterSprite.color = actorController.GetActorModel().ActorShooterColor;
        }

        public void SetPosition(Vector2 _position)
        {
            transform.position = _position;
        }

        public void ShowView()
        {
            gameObject.SetActive(true);
        }

        public void HideView()
        {
            gameObject.SetActive(false);
        }
        public void IdleAnimation()
        {
            actorAnimator.Play(IDLE_HASH, 0, 0f);
        }
        public void ShootAnimation()
        {
            shooterAnimator.Play(SHOOT_HASH, 0, 0f);
        }

        private void OnTriggerEnter2D(Collider2D _collider)
        {
            if (_collider.CompareTag("Projectile"))
            {
                // Avoid collision with the owner
                ProjectileView projectileView = _collider.gameObject.GetComponent<ProjectileView>();
                if ((projectileView.projectileController.GetProjectileModel().ProjectileOwnerActor == ActorType.Player)
                    && (actorController.GetActorModel().ActorType == ActorType.Player)) return;
                if ((projectileView.projectileController.GetProjectileModel().ProjectileOwnerActor != ActorType.Player)
                    && (actorController.GetActorModel().ActorType != ActorType.Player)) return;

                if (!actorController.GetActorModel().IsShieldActive)
                {
                    actorController.DecreaseHealth(); // Decrease actor's health on hit
                }

                if (actorController.GetActorModel().ActorType != ActorType.Player)
                {
                    actorController.AddScore(projectileView.projectileController.GetProjectileModel().HitScore);
                }
            }
        }

        // Getters
        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}