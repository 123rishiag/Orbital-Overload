using ServiceLocator.Bullet;
using ServiceLocator.Main;
using ServiceLocator.Sound;
using ServiceLocator.UI;

namespace ServiceLocator.Player
{
    public class PlayerService
    {
        // Private Variables
        private PlayerController playerController;

        // Private Services

        public PlayerService(PlayerConfig _playerConfig,
            GameService _gameService, SoundService _soundService, UIService _uiService,
            BulletService _bulletService)
        {
            // Setting Variables
            playerController = new PlayerController(_playerConfig,
            _gameService, _soundService, _uiService,
            _bulletService);
        }

        public void Update()
        {
            playerController.Update();
        }
        public void FixedUpdate()
        {
            playerController.FixedUpdate();
        }

        // Getters
        public PlayerController GetPlayerController() => playerController;
    }
}