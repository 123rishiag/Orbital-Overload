using ServiceLocator.Player;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class PowerUpService
    {
        // Private Variables
        private PowerUpConfig powerUpConfig;
        private float powerUpSpawnTimer;

        // Private Services
        private PlayerService playerService;

        public PowerUpService(PowerUpConfig _powerUpConfig, PlayerService _playerService)
        {
            // Setting Variables
            powerUpConfig = _powerUpConfig;

            // Setting Services
            playerService = _playerService;

            // Setting Elements
            powerUpSpawnTimer = powerUpConfig.powerUpSpawnInterval;
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
            // Fetching Data
            int index = Random.Range(0, powerUpConfig.powerUpData.Length);
            PowerUpData powerUpData = powerUpConfig.powerUpData[index];

            // Fetching Position & Direction
            Vector2 randomDirection = new Vector2(
                    Random.Range(0, 2) == 0 ? -1 : 1,
                    Random.Range(0, 2) == 0 ? -1 : 1
                    );
            Vector2 awayFromPlayerOffset = randomDirection * powerUpConfig.powerUpAwayFromPlayerSpawnDistance;
            Vector2 playerPosition = playerService.GetPlayerController().GetPlayerView().GetPosition();
            Vector2 spawnPosition = playerPosition + awayFromPlayerOffset +
                Random.insideUnitCircle * powerUpConfig.powerUpSpawnRadius;

            // Instantiating Object
            GameObject powerUp = GameObject.Instantiate(powerUpConfig.powerUpPrefab, spawnPosition, Quaternion.identity);

            // Creating Controller
            PowerUpController powerUpController = powerUp.GetComponent<PowerUpController>();
            powerUpController.Init(powerUpData);
        }


    }
}