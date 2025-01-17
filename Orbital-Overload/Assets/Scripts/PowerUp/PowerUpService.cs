using ServiceLocator.Main;
using ServiceLocator.Sound;
using ServiceLocator.Spawn;
using ServiceLocator.UI;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class PowerUpService
    {
        // Private Variables
        private PowerUpConfig powerUpConfig;
        private Transform powerUpParentPanel;
        private PowerUpPool powerUpPool;

        // Private Services
        private GameService gameService;
        private SoundService soundService;
        private UIService uiService;
        private SpawnService spawnService;

        public PowerUpService(PowerUpConfig _powerUpConfig, Transform _powerUpParentPanel)
        {
            // Setting Variables
            powerUpConfig = _powerUpConfig;
            powerUpParentPanel = _powerUpParentPanel;
        }

        public void Init(GameService _gameService, SoundService _soundService, UIService _uiService, SpawnService _spawnService)
        {
            // Setting Services
            gameService = _gameService;
            soundService = _soundService;
            uiService = _uiService;
            spawnService = _spawnService;

            // Setting Elements

            // Creating Object Pool for powerups
            powerUpPool = new PowerUpPool(powerUpConfig, powerUpParentPanel, gameService, soundService, uiService);

            // Creating spawn controller for powerups
            spawnService.CreateSpawnController(powerUpConfig.powerUpSpawnInterval, powerUpConfig.powerUpSpawnRadius,
                powerUpConfig.powerUpAwayFromPlayerSpawnDistance, CreatePowerUp);
        }

        private void CreatePowerUp(Vector2 _spawnPosition)
        {
            powerUpPool.GetPowerUp(_spawnPosition);
        }
        public void Update()
        {
            ProcessPowerUpUpdate();
        }

        private void ProcessPowerUpUpdate()
        {
            for (int i = powerUpPool.pooledItems.Count - 1; i >= 0; i--)
            {
                // Skipping if the pooled item's isUsed is false
                if (!powerUpPool.pooledItems[i].isUsed)
                {
                    continue;
                }

                var powerUpController = powerUpPool.pooledItems[i].Item;

                if (!powerUpController.IsActive())
                {
                    ReturnPowerUpToPool(powerUpController);
                }
            }
        }

        private void ReturnPowerUpToPool(PowerUpController _powerUpToReturn)
        {
            _powerUpToReturn.GetPowerUpView().HideView();
            powerUpPool.ReturnItem(_powerUpToReturn);
        }
    }
}