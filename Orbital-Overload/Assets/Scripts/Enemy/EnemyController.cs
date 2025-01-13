using ServiceLocator.Bullet;
using ServiceLocator.Player;
using UnityEngine;

namespace ServiceLocator.Enemy
{
    public class EnemyController
    {
        // Private Variables
        private EnemyModel enemyModel;
        private EnemyView enemyView;

        private float lastShootTime; // Time of last shot
        public float moveX; // X-axis movement input
        public float moveY; // Y-axis movement input
        public bool isShooting; // Shooting state
        public Vector2 mouseDirection; // Direction of the mouse
        public float enemyAwayFromPlayerMinDistance; // Minimum distance from player enemy should maintain

        // Private Services
        private BulletService bulletService;
        private PlayerService playerService;

        public EnemyController(EnemyConfig _enemyConfig, Vector2 _spawnPosition,
            BulletService _bulletService, PlayerService _playerService)
        {
            // Setting Variables
            enemyModel = new EnemyModel(_enemyConfig.enemyData);
            enemyView = GameObject.Instantiate(_enemyConfig.enemyPrefab, _spawnPosition, Quaternion.identity).
                GetComponent<EnemyView>();
            enemyView.Init(this);

            lastShootTime = 0f;
            moveX = 0f;
            moveY = 0f;
            isShooting = false;
            mouseDirection = Vector2.zero;
            enemyAwayFromPlayerMinDistance = _enemyConfig.enemyAwayFromPlayerMinDistance;

            // Setting Services
            bulletService = _bulletService;
            playerService = _playerService;
        }

        public void Update()
        {
            MovementInput(); // Calculate movement direction towards player
            ShootInput(); // Handle shoot input
            RotateInput(); // Update rotation direction
        }

        public void FixedUpdate()
        {
            Move(); // Move enemy
            Shoot(); // Shoot bullets
            Rotate(); // Rotate enemy towards player
        }

        private void MovementInput()
        {
            Vector2 playerPosition = playerService.GetPlayerController().GetPlayerView().GetPosition();
            float distanceToPlayer = Vector2.Distance(enemyView.transform.position, playerPosition);
            if (distanceToPlayer > enemyAwayFromPlayerMinDistance)
            {
                Vector2 direction = (playerPosition - (Vector2)enemyView.transform.position).normalized;
                moveX = direction.x;
                moveY = direction.y;
            }
            else
            {
                moveX = 0.0f;
                moveY = 0.0f;
            }
        }
        private void ShootInput() { }
        private void RotateInput()
        {
            Vector2 playerPosition = playerService.GetPlayerController().GetPlayerView().GetPosition();
            mouseDirection = (playerPosition - (Vector2)enemyView.transform.position).normalized;
        }

        private void Move()
        {
            Vector2 moveVector = new Vector2(moveX, moveY) * enemyModel.MoveSpeed * Time.fixedDeltaTime;
            enemyView.transform.Translate(moveVector, Space.World);
        }
        private void Shoot()
        {
            if (Time.time >= lastShootTime + enemyModel.ShootCooldown)
            {
                lastShootTime = Time.time;
                bulletService.Shoot(enemyView.gameObject.tag, enemyModel.ShootSpeed, false, enemyView.shootPoint);
            }
        }
        private void Rotate()
        {
            float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
            enemyView.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }

        // Getters
        public EnemyModel GetEnemyModel() => enemyModel;
        public EnemyView GetEnemyView() => enemyView;
    }
}