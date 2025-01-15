using ServiceLocator.Actor;
using UnityEngine;

namespace ServiceLocator.Projectile
{
    public class ProjectileView : MonoBehaviour
    {
        // Private Variables
        public Rigidbody2D rigidBody; // Rigidbody2D component of the projectile// Private Variables
        public ProjectileController projectileController;

        public void Init(ProjectileController _projectileController)
        {
            // Setting Variables
            projectileController = _projectileController;
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
            if (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1)
            {
                Destroy(gameObject); // Destroying the object if it is off-screen
            }
        }

        private void OnTriggerEnter2D(Collider2D _collider)
        {
            if (_collider.CompareTag("Actor"))
            {
                // Avoid collision with the owner
                ActorView actorView = _collider.gameObject.GetComponent<ActorView>();
                if (actorView.actorController.GetActorModel().ActorType ==
                    projectileController.GetProjectileModel().ProjectileOwnerActor) return;

                Destroy(gameObject);
            }
            else if (_collider.CompareTag("Projectile"))
            {
                Destroy(gameObject);
            }
        }
    }
}