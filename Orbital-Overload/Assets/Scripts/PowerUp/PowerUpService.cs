using ServiceLocator.Main;
using ServiceLocator.Player;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using System.Collections;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class PowerUpService
    {
        // Private Variables
        private PowerUpConfig powerUpConfig;
        private float powerUpSpawnTimer;

        // Private Services
        private GameService gameService;
        private SoundService soundService;
        private UIService uiService;
        private PlayerService playerService;

        public PowerUpService(PowerUpConfig _powerUpConfig,
            GameService _gameService, SoundService _soundService, UIService _uiService, PlayerService _playerService)
        {
            // Setting Variables
            powerUpConfig = _powerUpConfig;

            // Setting Services
            gameService = _gameService;
            soundService = _soundService;
            uiService = _uiService;
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
            Vector2 playerPosition = playerService.GetPlayerController().GetPosition();
            Vector2 spawnPosition = playerPosition + awayFromPlayerOffset +
                Random.insideUnitCircle * powerUpConfig.powerUpSpawnRadius;

            // Instantiating Object
            GameObject powerUp = GameObject.Instantiate(powerUpConfig.powerUpPrefab, spawnPosition, Quaternion.identity);

            // Creating Controller
            PowerUpController powerUpController = powerUp.GetComponent<PowerUpController>();
            powerUpController.Init(powerUpData, soundService, this);
        }

        public void ActivatePowerUp(PowerUpData _powerUpData)
        {
            gameService.StartManagedCoroutine(PowerUp(_powerUpData)); // Activate power-up effect
        }

        private IEnumerator PowerUp(PowerUpData _powerUpData)
        {
            string powerUpText;
            if (_powerUpData.powerUpType == PowerUpType.HealthPick || _powerUpData.powerUpType == PowerUpType.Teleport)
            {
                powerUpText = _powerUpData.powerUpType.ToString() + "ed.";
            }
            else
            {
                powerUpText = _powerUpData.powerUpType.ToString() + " activated for " + _powerUpData.powerUpDuration.ToString() + " seconds.";
            }
            uiService.GetUIController().UpdatePowerUpText(powerUpText);
            switch (_powerUpData.powerUpType)
            {
                case PowerUpType.HealthPick:
                    playerService.GetPlayerController().IncreaseHealth((int)_powerUpData.powerUpValue); // Increase health
                    yield return new WaitForSeconds(_powerUpData.powerUpDuration);
                    break;
                case PowerUpType.HomingOrbs:
                    playerService.GetPlayerController().isHoming = true; // Activate homing bullets
                    yield return new WaitForSeconds(_powerUpData.powerUpDuration);
                    playerService.GetPlayerController().isHoming = false; // Deactivate homing bullets
                    break;
                case PowerUpType.RapidFire:
                    playerService.GetPlayerController().shootCooldown /= _powerUpData.powerUpValue; // Increase fire rate
                    yield return new WaitForSeconds(_powerUpData.powerUpDuration);
                    playerService.GetPlayerController().shootCooldown *= _powerUpData.powerUpValue; // Reset fire rate
                    break;
                case PowerUpType.Shield:
                    playerService.GetPlayerController().isShieldActive = true; // Activate shield
                    yield return new WaitForSeconds(_powerUpData.powerUpDuration);
                    playerService.GetPlayerController().isShieldActive = false; // Deactivate shield
                    break;
                case PowerUpType.SlowMotion:
                    Time.timeScale = _powerUpData.powerUpValue; // Slow down time
                    yield return new WaitForSecondsRealtime(_powerUpData.powerUpDuration);
                    Time.timeScale = 1f; // Reset time
                    break;
                case PowerUpType.Teleport:
                    playerService.GetPlayerController().Teleport(_powerUpData.powerUpValue); // Teleport player
                    yield return new WaitForSeconds(_powerUpData.powerUpDuration);
                    break;
                default:
                    yield return new WaitForSeconds(1);
                    break;
            }
            uiService.GetUIController().HidePowerUpText(); // Hide power-up text
        }
    }
}