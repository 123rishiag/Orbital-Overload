using ServiceLocator.Actor;
using ServiceLocator.Main;
using ServiceLocator.Sound;
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
        private ActorService actorService;

        public PowerUpService(PowerUpConfig _powerUpConfig,
            GameService _gameService, SoundService _soundService, UIService _uiService, ActorService _actorService)
        {
            // Setting Variables
            powerUpConfig = _powerUpConfig;
            powerUps = new List<PowerUpController>();
            powerUpSpawnTimer = powerUpConfig.powerUpSpawnInterval;

            // Setting Services
            gameService = _gameService;
            soundService = _soundService;
            uiService = _uiService;
            actorService = _actorService;
        }

        public void Update()
        {
            // Accumulate time
            powerUpSpawnTimer -= Time.deltaTime;

            // Check if the spawn interval has passed
            if (powerUpSpawnTimer < 0)
            {
                powerUpSpawnTimer = powerUpConfig.powerUpSpawnInterval; // Reset the timer
                SpawnPowerUp();
            }
        }

        private void SpawnPowerUp()
        {
            // Fetching Index
            int powerUpIndex = Random.Range(0, powerUpConfig.powerUpData.Length);

            // Fetching Position & Direction
            Vector2 randomDirection = new Vector2(
                    Random.Range(0, 2) == 0 ? -1 : 1,
                    Random.Range(0, 2) == 0 ? -1 : 1
                    );
            Vector2 awayFromPlayerOffset = randomDirection * powerUpConfig.powerUpAwayFromPlayerSpawnDistance;
            Vector2 playerPosition = actorService.GetPlayerActorController().GetActorView().GetPosition();
            Vector2 spawnPosition = playerPosition + awayFromPlayerOffset +
                Random.insideUnitCircle * powerUpConfig.powerUpSpawnRadius;

            // Creating Controller
            PowerUpController powerUpController = new PowerUpController(powerUpConfig, spawnPosition, powerUpIndex,
                gameService, soundService, uiService);
            powerUps.Add(powerUpController);
        }
    }
}