using ServiceLocator.Main;
using ServiceLocator.Player;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using System.Collections;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class PowerUpController
    {
        // Private Variables
        private PowerUpModel powerUpModel;
        private PowerUpView powerUpView;

        // Private Services
        private GameService gameService;
        private SoundService soundService;
        private UIService uiService;

        public PowerUpController(PowerUpConfig _powerUpConfig, Vector2 _spawnPosition, int _powerUpIndex,
            GameService _gameService, SoundService _soundService, UIService _uiService)
        {
            // Setting Variables
            powerUpModel = new PowerUpModel(_powerUpConfig.powerUpData[_powerUpIndex]);
            powerUpView = GameObject.Instantiate(_powerUpConfig.powerUpPrefab, _spawnPosition, Quaternion.identity).
                GetComponent<PowerUpView>();
            powerUpView.Init(this, powerUpModel.PowerUpColor);

            // Setting Services
            gameService = _gameService;
            soundService = _soundService;
            uiService = _uiService;
        }

        public void ActivatePowerUp(PlayerController _playerController)
        {
            gameService.StartManagedCoroutine(PowerUp(_playerController)); // Activate power-up effect
        }

        private IEnumerator PowerUp(PlayerController _playerController)
        {
            string powerUpText;
            if (powerUpModel.PowerUpType == PowerUpType.HealthPick || powerUpModel.PowerUpType == PowerUpType.Teleport)
            {
                powerUpText = powerUpModel.PowerUpType.ToString() + "ed.";
            }
            else
            {
                powerUpText = powerUpModel.PowerUpType.ToString() + " activated for " + powerUpModel.PowerUpDuration.ToString() + " seconds.";
            }
            soundService.PlaySoundEffect(SoundType.PowerUpPickup);
            uiService.GetUIController().GetUIView().UpdatePowerUpText(powerUpText);
            switch (powerUpModel.PowerUpType)
            {
                case PowerUpType.HealthPick:
                    _playerController.IncreaseHealth((int)powerUpModel.PowerUpValue); // Increase health
                    yield return new WaitForSeconds(powerUpModel.PowerUpDuration);
                    break;
                case PowerUpType.HomingOrbs:
                    _playerController.GetPlayerModel().IsHoming = true; // Activate homing bullets
                    yield return new WaitForSeconds(powerUpModel.PowerUpDuration);
                    _playerController.GetPlayerModel().IsHoming = false; // Deactivate homing bullets
                    break;
                case PowerUpType.RapidFire:
                    _playerController.GetPlayerModel().ShootCooldown /= powerUpModel.PowerUpValue; // Increase fire rate
                    yield return new WaitForSeconds(powerUpModel.PowerUpDuration);
                    _playerController.GetPlayerModel().ShootCooldown *= powerUpModel.PowerUpValue; // Reset fire rate
                    break;
                case PowerUpType.Shield:
                    _playerController.GetPlayerModel().IsShieldActive = true; // Activate shield
                    yield return new WaitForSeconds(powerUpModel.PowerUpDuration);
                    _playerController.GetPlayerModel().IsShieldActive = false; // Deactivate shield
                    break;
                case PowerUpType.SlowMotion:
                    Time.timeScale = powerUpModel.PowerUpValue; // Slow down time
                    yield return new WaitForSecondsRealtime(powerUpModel.PowerUpDuration);
                    Time.timeScale = 1f; // Reset time
                    break;
                case PowerUpType.Teleport:
                    _playerController.Teleport(powerUpModel.PowerUpValue); // Teleport player
                    yield return new WaitForSeconds(powerUpModel.PowerUpDuration);
                    break;
                default:
                    yield return new WaitForSeconds(1);
                    break;
            }
            uiService.GetUIController().GetUIView().HidePowerUpText(); // Hide power-up text
        }

        // Getters
        public PowerUpModel GetPowerUpModel() => powerUpModel;
        public PowerUpView GetPowerUpView() => powerUpView;
    }
}