using ServiceLocator.Bullet;
using ServiceLocator.Main;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using UnityEngine;

namespace ServiceLocator.Player
{
    public class PlayerService
    {
        // Private Variables
        private PlayerController playerController;

        // Private Services

        public PlayerService(PlayerConfig _playerConfig, GameService _gameService, SoundService _soundService, UIService _uiService,
            BulletService _bulletService)
        {
            // Setting Variables
            playerController = GameObject.Instantiate(_playerConfig.playerPrefab).GetComponent<PlayerController>();

            // Setting Elements
            playerController.Init(_playerConfig.playerData, _gameService, _soundService, _uiService, _bulletService);
        }

        public PlayerController GetPlayerController() => playerController;
    }
}