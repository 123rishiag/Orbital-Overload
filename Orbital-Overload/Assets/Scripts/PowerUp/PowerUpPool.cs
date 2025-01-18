using ServiceLocator.Event;
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
        private EventService eventService;

        public PowerUpPool(PowerUpConfig _powerUpConfig, Transform _powerUpParentPanel,
            EventService _eventService)
        {
            // Setting Variables
            powerUpConfig = _powerUpConfig;
            powerUpParentPanel = _powerUpParentPanel;

            // Setting Services
            eventService = _eventService;
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
                        eventService
                        );
                case PowerUpType.HomingOrbs:
                    return new HomingOrbsPowerUpController(powerUpConfig.powerUpData[powerUpIndex], powerUpConfig.powerUpPrefab,
                        powerUpParentPanel, spawnPosition,
                        eventService
                        );
                case PowerUpType.RapidFire:
                    return new RapidFirePowerUpController(powerUpConfig.powerUpData[powerUpIndex], powerUpConfig.powerUpPrefab,
                        powerUpParentPanel, spawnPosition,
                        eventService
                        );
                case PowerUpType.Shield:
                    return new ShieldPowerUpController(powerUpConfig.powerUpData[powerUpIndex], powerUpConfig.powerUpPrefab,
                        powerUpParentPanel, spawnPosition,
                        eventService
                        );
                case PowerUpType.SlowMotion:
                    return new SlowMotionPowerUpController(powerUpConfig.powerUpData[powerUpIndex], powerUpConfig.powerUpPrefab,
                        powerUpParentPanel, spawnPosition,
                        eventService
                        );
                case PowerUpType.Teleport:
                    return new TeleportPowerUpController(powerUpConfig.powerUpData[powerUpIndex], powerUpConfig.powerUpPrefab,
                        powerUpParentPanel, spawnPosition,
                        eventService
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