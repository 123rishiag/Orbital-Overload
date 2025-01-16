using ServiceLocator.Projectile;
using UnityEngine;

namespace ServiceLocator.Actor
{
    public class ActorView : MonoBehaviour
    {
        [SerializeField] public Transform shootPoint; // Point from where projectiles are shot
        [SerializeField] public SpriteRenderer actorSprite; // Actor Sprite
        [SerializeField] public SpriteRenderer actorShooterSprite; // Actor's Shooter Sprite

        [HideInInspector]
        public ActorController actorController;

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

        private void OnTriggerEnter2D(Collider2D _collider)
        {
            if (_collider.CompareTag("Projectile"))
            {
                // Avoid collision with the owner
                ProjectileView projectileView = _collider.gameObject.GetComponent<ProjectileView>();
                if (projectileView.projectileController.GetProjectileModel().ProjectileOwnerActor ==
                    actorController.GetActorModel().ActorType) return;

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