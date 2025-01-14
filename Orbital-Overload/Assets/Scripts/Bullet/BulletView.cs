using ServiceLocator.Actor;
using UnityEngine;

namespace ServiceLocator.Bullet
{
    public class BulletView : MonoBehaviour
    {
        // Private Variables
        public Rigidbody2D rigidBody; // Rigidbody2D component of the bullet// Private Variables
        public BulletController bulletController;

        public void Init(BulletController _bulletController)
        {
            // Setting Variables
            bulletController = _bulletController;
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D _collider)
        {
            if (_collider.CompareTag("Actor"))
            {
                // Avoid collision with the owner
                ActorView actorView = _collider.gameObject.GetComponent<ActorView>();
                if (actorView.actorController.GetActorModel().ActorType ==
                    bulletController.GetBulletModel().BulletOwnerActor) return;

                Destroy(gameObject);
            }
            else if (_collider.CompareTag("Bullet"))
            {
                Destroy(gameObject);
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject); // Destroy bullet when it goes off screen
        }
    }
}