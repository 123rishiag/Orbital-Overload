using ServiceLocator.Bullet;
using ServiceLocator.Player;
using UnityEngine;

namespace ServiceLocator.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Transform shootPoint; // Point from where bullets are shot

        // Private Variables
        private EnemyData enemyData;

        private float moveX = 0f; // X-axis movement
        private float moveY = 0f; // Y-axis movement
        private float lastShootTime = 0f; // Time of last shot
        private Vector2 mouseDirection = Vector2.zero; // Direction towards the player

        // Private Services
        private BulletService bulletService;
        private PlayerService playerService;

        public void Init(EnemyData _enemyData, BulletService _bulletService, PlayerService _playerService)
        {
            // Setting Variables
            enemyData = _enemyData;

            // Setting Services
            bulletService = _bulletService;
            playerService = _playerService;
        }

        private void Update()
        {
            MoveTowardsPlayerDirection(); // Calculate movement direction towards player
            RotateInput(); // Update rotation direction
        }

        private void FixedUpdate()
        {
            Move(); // Move enemy
            Shoot(); // Shoot bullets
            Rotate(); // Rotate enemy towards player
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject); // Destroy enemy when it goes off screen
        }

        private void MoveTowardsPlayerDirection()
        {
            Vector2 playerPosition = playerService.GetPlayerController().GetPlayerView().GetPosition();
            float distanceToPlayer = Vector2.Distance(transform.position, playerPosition);
            if (distanceToPlayer > enemyData.enemyAwayFromPlayerMinDistance)
            {
                Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;
                moveX = direction.x;
                moveY = direction.y;
            }
            else
            {
                moveX = 0.0f;
                moveY = 0.0f;
            }
        }

        private void Move()
        {
            Vector2 moveVector = new Vector2(moveX, moveY) * enemyData.moveSpeed * Time.fixedDeltaTime;
            transform.Translate(moveVector, Space.World);
        }

        private void Shoot()
        {
            if (Time.time >= lastShootTime + enemyData.shootCooldown)
            {
                lastShootTime = Time.time;
                bulletService.Shoot(gameObject.tag, enemyData.shootSpeed, false, shootPoint);
            }
        }

        private void RotateInput()
        {
            Vector2 playerPosition = playerService.GetPlayerController().GetPlayerView().GetPosition();
            mouseDirection = (playerPosition - (Vector2)transform.position).normalized;
        }

        private void Rotate()
        {
            float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }
    }
}