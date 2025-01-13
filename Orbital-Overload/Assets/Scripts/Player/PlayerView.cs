using ServiceLocator.Bullet;
using UnityEngine;

namespace ServiceLocator.Player
{
    public class PlayerView : MonoBehaviour
    {
        // Private Variables
        [SerializeField] public Transform shootPoint; // Point from where bullets are shot
        public PlayerController playerController;

        // Getters & Setters
        public float MoveX { get; private set; } // X-axis movement input
        public float MoveY { get; private set; } // Y-axis movement input
        public bool IsShooting { get; private set; } // Shooting state
        public Vector2 MouseDirection { get; private set; } // Direction of the mouse

        public void Init(PlayerController _playerController)
        {
            playerController = _playerController;
            MoveX = 0f;    // X-axis movement input
            MoveY = 0f;    // Y-axis movement input
            IsShooting = false; // Shooting state
            MouseDirection = Vector2.zero; // Direction of the mouse
        }

        public void MovementInput()
        {
            MoveX = Input.GetAxis("Horizontal"); // Get horizontal input
            MoveY = Input.GetAxis("Vertical"); // Get vertical input
            if (MoveX == 0f)
            {
                MoveX = playerController.GetPlayerModel().CasualMoveSpeed; // Default move speed
            }
        }
        public void ShootInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                IsShooting = true; // Start shooting
            }
            else if (Input.GetMouseButtonUp(0))
            {
                IsShooting = false; // Stop shooting
            }
        }
        public void RotateInput()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Ensure z is zero for 2D
            MouseDirection = (mousePosition - transform.position).normalized;
        }

        private void OnTriggerEnter2D(Collider2D _collider)
        {
            if (_collider.CompareTag("Bullet"))
            {
                // Avoid collision with the owner
                BulletView bulletView = _collider.gameObject.GetComponent<BulletView>();
                if (bulletView.bulletController.GetBulletModel().BulletOwnerTag == gameObject.tag) return;

                if (_collider.CompareTag("Player")) return;
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