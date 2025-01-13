using ServiceLocator.Bullet;
using ServiceLocator.Main;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using UnityEngine;

namespace ServiceLocator.Player
{
    public class PlayerController
    {
        // Private Variables
        private PlayerModel playerModel;
        private PlayerView playerView;
        private float lastShootTime; // Time of last shot

        // Private Services
        private GameService gameService;
        private SoundService soundService;
        private UIService uiService;
        private BulletService bulletService;

        public PlayerController(PlayerConfig _playerConfig,
            GameService _gameService, SoundService _soundService, UIService _uiService,
            BulletService _bulletService)
        {
            // Setting Variables
            playerModel = new PlayerModel(_playerConfig.playerData);
            playerView = GameObject.Instantiate(_playerConfig.playerPrefab).GetComponent<PlayerView>();
            playerView.Init(this);
            lastShootTime = 0f;

            // Setting Services
            gameService = _gameService;
            soundService = _soundService;
            uiService = _uiService;
            bulletService = _bulletService;

            // Setting Elements
            uiService.GetUIController().GetUIView().UpdateHealthText(playerModel.CurrentHealth); // Update health text
        }

        public void Update()
        {
            playerView.MovementInput(); // Handle movement input
            playerView.ShootInput(); // Handle shoot input
            playerView.RotateInput(); // Handle rotate input
        }
        public void FixedUpdate()
        {
            Move(); // Handle movement
            Shoot(); // Handle shooting
            Rotate(); // Handle rotation
        }

        private void Move()
        {
            Vector2 moveVector = new Vector2(playerView.MoveX, playerView.MoveY) * playerModel.MoveSpeed * Time.fixedDeltaTime;
            playerView.transform.Translate(moveVector, Space.World); // Move player
        }
        private void Shoot()
        {
            if (playerView.IsShooting && Time.time >= lastShootTime + playerModel.ShootCooldown)
            {
                lastShootTime = Time.time; // Update last shoot time
                bulletService.Shoot(playerView.gameObject.tag, playerModel.ShootSpeed, playerModel.IsHoming,
                    playerView.shootPoint);
            }
        }
        private void Rotate()
        {
            float angle = Mathf.Atan2(playerView.MouseDirection.y, playerView.MouseDirection.x) * Mathf.Rad2Deg;
            playerView.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Rotate player to face mouse
        }

        public void Teleport(float _minDistance)
        {
            float maxDistance = _minDistance + 3f;

            float angle = Random.Range(0f, Mathf.PI * 2);

            float distance = Random.Range(_minDistance, maxDistance);

            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance;
            Vector3 newPosition = playerView.transform.position + offset;

            newPosition.x = Mathf.Clamp(newPosition.x, -8f, 8f); // Clamp x position
            newPosition.y = Mathf.Clamp(newPosition.y, -4f, 4f); // Clamp y position

            playerView.transform.position = newPosition; // Teleport player
        }

        public void AddScore()
        {
            playerModel.CurrentScore += playerModel.IncreaseScoreValue; // Increase score
            uiService.GetUIController().GetUIView().UpdateScoreText(playerModel.CurrentScore); // Update score text
        }

        public void DecreaseHealth()
        {
            playerModel.CurrentHealth -= 1; // Decrease health
            if (playerModel.CurrentHealth < 0)
            {
                playerModel.CurrentHealth = 0;
            }
            if (playerModel.CurrentHealth == 0)
            {
                PlayerDie(); // Handle player death
            }
            soundService.PlaySoundEffect(SoundType.PlayerHurt);
            uiService.GetUIController().GetUIView().UpdateHealthText(playerModel.CurrentHealth);
        }

        public void IncreaseHealth(int _increaseHealth)
        {
            playerModel.CurrentHealth += _increaseHealth; // Increase health
            if (playerModel.CurrentHealth > playerModel.MaxHealth)
            {
                playerModel.CurrentHealth = playerModel.MaxHealth; // Clamp health to max
            }
            else
            {
                soundService.PlaySoundEffect(SoundType.PlayerHeal); // Play heal sound effect
            }
            uiService.GetUIController().GetUIView().UpdateHealthText(playerModel.CurrentHealth);
        }

        private void PlayerDie()
        {
            gameService.GetGameController().GameOver(); // Trigger game over
        }

        // Getters
        public PlayerModel GetPlayerModel() => playerModel;
        public PlayerView GetPlayerView() => playerView;
    }
}