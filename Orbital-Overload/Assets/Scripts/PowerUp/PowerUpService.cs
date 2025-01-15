using ServiceLocator.Main;
using ServiceLocator.Sound;
using ServiceLocator.Spawn;
using ServiceLocator.UI;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class PowerUpService
    {
        // Private Variables
        private PowerUpConfig powerUpConfig;
        private List<PowerUpController> powerUps;
        private float powerUpSpawnTimer;

        // Private Services
        private GameService gameService;
        private SoundService soundService;
        private UIService uiService;
        private SpawnService spawnService;

        public PowerUpService(PowerUpConfig _powerUpConfig)
        {
            // Setting Variables
            powerUpConfig = _powerUpConfig;
            powerUps = new List<PowerUpController>();
            powerUpSpawnTimer = powerUpConfig.powerUpSpawnInterval;
        }

        public void Init(GameService _gameService, SoundService _soundService, UIService _uiService, SpawnService _spawnService)
        {
            // Setting Services
            gameService = _gameService;
            soundService = _soundService;
            uiService = _uiService;
            spawnService = _spawnService;

            // Creating spawn controller for powerups
            spawnService.CreateSpawnController(powerUpConfig.powerUpSpawnInterval, powerUpConfig.powerUpSpawnRadius,
                powerUpConfig.powerUpAwayFromPlayerSpawnDistance, CreatePowerUp);
        }

        private void CreatePowerUp(Vector2 _spawnPosition)
        {
            // Fetching Random Index
            int powerUpIndex = Random.Range(0, powerUpConfig.powerUpData.Length);

            // Creating Controller
            PowerUpController powerUpController = new PowerUpController(powerUpConfig, _spawnPosition, powerUpIndex,
                gameService, soundService, uiService);
            powerUps.Add(powerUpController);
        }
    }
}