using ServiceLocator.Bullet;
using UnityEngine;

namespace ServiceLocator.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] public Transform shootPoint; // Point from where bullets are shot

        // Private Variables
        public PlayerController playerController;

        public void Init(PlayerController _playerController)
        {
            // Setting Variables
            playerController = _playerController;
        }

        private void OnTriggerEnter2D(Collider2D _collider)
        {
            if (_collider.CompareTag("Bullet"))
            {
                // Avoid collision with the owner
                BulletView bulletView = _collider.gameObject.GetComponent<BulletView>();
                if (bulletView.bulletController.GetBulletModel().BulletOwnerTag == gameObject.tag) return;

                if (!playerController.GetPlayerModel().IsShieldActive)
                {
                    playerController.DecreaseHealth(); // Decrease player's health on hit
                }
            }
        }

        // Getters
        public Vector2 GetPosition()
        {
            return transform.position;
        }
    }
}