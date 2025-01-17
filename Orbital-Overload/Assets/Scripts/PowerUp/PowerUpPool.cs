using ServiceLocator.Main;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Utility;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class PowerUpPool : GenericObjectPool<PowerUpController>
    {
        // Private Variables
        private PowerUpConfig powerUpConfig;
        private Transform powerUpParentPanel;

        private Vector2 spawnPosition;

        // Private Services
        private GameService gameService;
        private SoundService soundService;
        private UIService uiService;

        public PowerUpPool(PowerUpConfig _powerUpConfig, Transform _powerUpParentPanel,
            GameService _gameService, SoundService _soundService, UIService _uiService)
        {
            // Setting Variables
            powerUpConfig = _powerUpConfig;
            powerUpParentPanel = _powerUpParentPanel;

            // Setting Services
            gameService = _gameService;
            soundService = _soundService;
            uiService = _uiService;
        }

        public PowerUpController GetPowerUp(Vector2 _spawnPosition)
        {
            // Setting Variables
            spawnPosition = _spawnPosition;

            // Fetching Item
            var item = GetItem<PowerUpController>();

            // Fetching Index
            int powerUpIndex = GetRandomPowerUpIndex();

            // Resetting Item Properties
            item.Reset(powerUpConfig.powerUpData[powerUpIndex], spawnPosition);

            return item;
        }

        protected override PowerUpController CreateItem<T>()
        {
            // Fetching Index
            int powerUpIndex = GetRandomPowerUpIndex();

            // Creating Controller
            return new PowerUpController(powerUpConfig.powerUpData[powerUpIndex], powerUpConfig.powerUpPrefab,
                powerUpParentPanel, spawnPosition,
            gameService, soundService, uiService
            );
        }

        // Getters
        private int GetRandomPowerUpIndex()
        {
            // Fetching Random Index
            return Random.Range(0, powerUpConfig.powerUpData.Length);
        }
    }
}