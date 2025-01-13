using ServiceLocator.Bullet;
using ServiceLocator.Player;
using UnityEngine;

namespace ServiceLocator.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] public Transform shootPoint; // Point from where bullets are shot

        // Private Variables
        private EnemyController enemyController;

        public void Init(EnemyController _enemyController)
        {
            // Setting Variables
            enemyController = _enemyController;
        }

        private void OnTriggerEnter2D(Collider2D _collider)
        {
            if (_collider.CompareTag("Bullet"))
            {
                // Avoid collision with the owner
                BulletView bulletView = _collider.gameObject.GetComponent<BulletView>();
                if (bulletView.bulletController.GetBulletModel().BulletOwnerTag == gameObject.tag) return;

                PlayerView playerView = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerView>();
                playerView.playerController.AddScore(enemyController.GetEnemyModel().HitScore); // Increase player score on hit
                Destroy(gameObject); // Destroy bullet on hit
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject); // Destroy enemy when it goes off screen
        }
    }
}