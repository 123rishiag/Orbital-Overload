using ServiceLocator.Event;
using ServiceLocator.Spawn;
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
        private SpawnService spawnService;

        public PowerUpService(PowerUpConfig _powerUpConfig, Transform _powerUpParentPanel)
        {
            // Setting Variables
            powerUpConfig = _powerUpConfig;
            powerUpParentPanel = _powerUpParentPanel;
        }

        public void Init(EventService _eventService, SpawnService _spawnService)
        {
            // Setting Services
            spawnService = _spawnService;

            // Setting Elements

            // Creating Object Pool for powerups
            powerUpPool = new PowerUpPool(powerUpConfig, powerUpParentPanel, _eventService);

            // Creating spawn controller for powerups
            spawnService.CreateSpawnController(powerUpConfig.powerUpSpawnInterval, powerUpConfig.powerUpSpawnRadius,
                powerUpConfig.powerUpAwayFromPlayerSpawnDistance, CreatePowerUp);
        }

        private void CreatePowerUp(Vector2 _spawnPosition)
        {
            // Fetching Random Index
            int powerUpIndex = Random.Range(0, powerUpConfig.powerUpData.Length);
            PowerUpType powerUpType = powerUpConfig.powerUpData[powerUpIndex].powerUpType;

            // Fetching PowerUp
            switch (powerUpType)
            {
                case PowerUpType.HealthPick:
                    powerUpPool.GetPowerUp<HealthPickPowerUpController>(_spawnPosition, powerUpType);
                    break;
                case PowerUpType.HomingOrbs:
                    powerUpPool.GetPowerUp<HomingOrbsPowerUpController>(_spawnPosition, powerUpType);
                    break;
                case PowerUpType.RapidFire:
                    powerUpPool.GetPowerUp<RapidFirePowerUpController>(_spawnPosition, powerUpType);
                    break;
                case PowerUpType.Shield:
                    powerUpPool.GetPowerUp<ShieldPowerUpController>(_spawnPosition, powerUpType);
                    break;
                case PowerUpType.SlowMotion:
                    powerUpPool.GetPowerUp<SlowMotionPowerUpController>(_spawnPosition, powerUpType);
                    break;
                case PowerUpType.Teleport:
                    powerUpPool.GetPowerUp<TeleportPowerUpController>(_spawnPosition, powerUpType);
                    break;
                default:
                    Debug.LogWarning($"Unhandled PowerUpType: {powerUpType}");
                    break;
            }
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

        public void Reset()
        {
            // Disabling All PowerUps
            for (int i = powerUpPool.pooledItems.Count - 1; i >= 0; i--)
            {
                // Skipping if the pooled item's isUsed is false
                if (!powerUpPool.pooledItems[i].isUsed)
                {
                    continue;
                }

                var powerUpController = powerUpPool.pooledItems[i].Item;
                ReturnPowerUpToPool(powerUpController);
            }
        }

        private void ReturnPowerUpToPool(PowerUpController _powerUpToReturn)
        {
            _powerUpToReturn.GetPowerUpView().HideView();
            powerUpPool.ReturnItem(_powerUpToReturn);
        }
    }
}