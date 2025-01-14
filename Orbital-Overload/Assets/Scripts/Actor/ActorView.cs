using ServiceLocator.Bullet;
using UnityEngine;

namespace ServiceLocator.Actor
{
    public class ActorView : MonoBehaviour
    {
        [SerializeField] public Transform shootPoint; // Point from where bullets are shot
        [SerializeField] public SpriteRenderer actorSprite; // Actor Sprite
        [SerializeField] public SpriteRenderer actorShooterSprite; // Actor's Shooter Sprite

        [HideInInspector]
        public ActorController actorController;

        public void Init(ActorController _actorController)
        {
            // Setting Variables
            actorController = _actorController;
            actorSprite.color = actorController.GetActorModel().ActorColor;
            actorShooterSprite.color = actorController.GetActorModel().ActorShooterColor;
        }

        private void OnTriggerEnter2D(Collider2D _collider)
        {
            if (_collider.CompareTag("Bullet"))
            {
                // Avoid collision with the owner
                BulletView bulletView = _collider.gameObject.GetComponent<BulletView>();
                if (bulletView.bulletController.GetBulletModel().BulletOwnerActor ==
                    actorController.GetActorModel().ActorType) return;

                if (!actorController.GetActorModel().IsShieldActive)
                {
                    actorController.DecreaseHealth(); // Decrease actor's health on hit
                }

                if (actorController.GetActorModel().ActorType != ActorType.Player)
                {
                    actorController.AddScore(bulletView.bulletController.GetBulletModel().HitScore);
                    if (!actorController.IsAlive())
                    {
                        Destroy(gameObject);
                    }
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