using ServiceLocator.Main;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Utility;
using System;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class PowerUpPool : GenericObjectPool<PowerUpController>
    {
        // Private Variables
        private PowerUpConfig powerUpConfig;
        private Transform powerUpParentPanel;

        private Vector2 spawnPosition;
        private PowerUpType powerUpType;

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

        public PowerUpController GetPowerUp<T>(Vector2 _spawnPosition, PowerUpType _powerUpType) where T : PowerUpController
        {
            // Setting Variables
            spawnPosition = _spawnPosition;
            powerUpType = _powerUpType;

            // Fetching Item
            var item = GetItem<T>();

            // Fetching Index
            int powerUpIndex = GetPowerUpIndex();

            // Resetting Item Properties
            item.Reset(powerUpConfig.powerUpData[powerUpIndex], spawnPosition);

            return item;
        }

        protected override PowerUpController CreateItem<T>()
        {
            // Fetching Index
            int powerUpIndex = GetPowerUpIndex();

            // Creating Controller
            switch (powerUpType)
            {
                case PowerUpType.HealthPick:
                    return new HealthPickPowerUpController(powerUpConfig.powerUpData[powerUpIndex], powerUpConfig.powerUpPrefab,
                        powerUpParentPanel, spawnPosition,
                        gameService, soundService, uiService
                        );
                case PowerUpType.HomingOrbs:
                    return new HomingOrbsPowerUpController(powerUpConfig.powerUpData[powerUpIndex], powerUpConfig.powerUpPrefab,
                        powerUpParentPanel, spawnPosition,
                        gameService, soundService, uiService
                        );
                case PowerUpType.RapidFire:
                    return new RapidFirePowerUpController(powerUpConfig.powerUpData[powerUpIndex], powerUpConfig.powerUpPrefab,
                        powerUpParentPanel, spawnPosition,
                        gameService, soundService, uiService
                        );
                case PowerUpType.Shield:
                    return new ShieldPowerUpController(powerUpConfig.powerUpData[powerUpIndex], powerUpConfig.powerUpPrefab,
                        powerUpParentPanel, spawnPosition,
                        gameService, soundService, uiService
                        );
                case PowerUpType.SlowMotion:
                    return new SlowMotionPowerUpController(powerUpConfig.powerUpData[powerUpIndex], powerUpConfig.powerUpPrefab,
                        powerUpParentPanel, spawnPosition,
                        gameService, soundService, uiService
                        );
                case PowerUpType.Teleport:
                    return new TeleportPowerUpController(powerUpConfig.powerUpData[powerUpIndex], powerUpConfig.powerUpPrefab,
                        powerUpParentPanel, spawnPosition,
                        gameService, soundService, uiService
                        );
                default:
                    Debug.LogWarning($"Unhandled PowerUpType: {powerUpType}");
                    return null;
            }
        }

        // Getters
        private int GetPowerUpIndex()
        {
            // Fetching Index
            return Array.FindIndex(powerUpConfig.powerUpData, data => data.powerUpType == powerUpType);
        }
    }
}