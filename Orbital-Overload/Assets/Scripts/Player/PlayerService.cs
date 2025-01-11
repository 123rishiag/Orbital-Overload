using ServiceLocator.Bullet;
using ServiceLocator.Sound;
using ServiceLocator.UI;

namespace ServiceLocator.Player
{
    public class PlayerService
    {
        // Private Variables
        private PlayerController playerController;

        // Private Services

        public PlayerService(PlayerConfig _playerConfig, SoundService _soundService, UIService _uiService,
            PlayerController _player, BulletService _bulletService)
        {
            // Setting Variables
            playerController = _player.GetComponent<PlayerController>();

            // Setting Elements
            playerController.Init(_playerConfig, _soundService, _uiService, _bulletService);
        }

        public PlayerController GetPlayerController() => playerController;
    }
}