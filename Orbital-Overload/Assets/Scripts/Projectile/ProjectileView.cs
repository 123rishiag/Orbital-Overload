using ServiceLocator.Actor;
using UnityEngine;

namespace ServiceLocator.Projectile
{
    public class ProjectileView : MonoBehaviour
    {
        [SerializeField] public Rigidbody2D rigidBody; // Rigidbody2D component of the projectile
        [SerializeField] public SpriteRenderer projectileSprite; // Projectile Sprite
        [HideInInspector]
        public ProjectileController projectileController;

        public void Init(ProjectileController _projectileController)
        {
            // Setting Variables
            projectileController = _projectileController;
            rigidBody = GetComponent<Rigidbody2D>();
            Reset();
        }

        public void Reset()
        {
            projectileSprite.color = projectileController.GetProjectileModel().ProjectileColor;
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
            if (_collider.CompareTag("Actor"))
            {
                // Avoid collision with the owner
                ActorView actorView = _collider.gameObject.GetComponent<ActorView>();
                if (actorView.actorController.GetActorModel().ActorType ==
                    projectileController.GetProjectileModel().ProjectileOwnerActor) return;

                HideView();
            }
            else if (_collider.CompareTag("Projectile"))
            {
                HideView();
            }
        }
    }
}