using ServiceLocator.Player;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using System.Collections;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class PowerUpController : MonoBehaviour
    {
        [SerializeField] public Material powerUpMaterial;

        // Private Variables
        private PowerUpType powerUpType;
        private float powerUpDuration;
        private float powerUpValue;
        private float powerUpLifetime;

        // Private Services
        private SoundService soundService;
        private UIService uiService;
        private PlayerService playerService;

        public void Init(PowerUpData _powerUpData, SoundService _soundService, UIService _uiService,
            PlayerService _playerService)
        {
            // Setting Variables
            powerUpType = _powerUpData.powerUpType;
            powerUpMaterial.color = _powerUpData.powerUpColor;
            powerUpDuration = _powerUpData.powerUpDuration;
            powerUpValue = _powerUpData.powerUpValue;
            powerUpLifetime = _powerUpData.powerUpLifetime;

            // Setting Services
            soundService = _soundService;
            uiService = _uiService;
            playerService = _playerService;
            
            GameObject.Destroy(gameObject, powerUpLifetime); // Destroy the power-up after its lifetime
        }

        private void ActivatePowerUp(PowerUpType powerUpType, float powerUpDuration, float powerUpValue)
        {
            StartCoroutine(PowerUp(powerUpType, powerUpDuration, powerUpValue)); // Activate power-up effect
        }

        private IEnumerator PowerUp(PowerUpType powerUpType, float powerUpDuration, float powerUpValue)
        {
            string powerUpText;
            if (powerUpType == PowerUpType.HealthPick || powerUpType == PowerUpType.Teleport)
            {
                powerUpText = powerUpType.ToString() + "ed.";
            }
            else
            {
                powerUpText = powerUpType.ToString() + " activated for " + powerUpDuration.ToString() + " seconds.";
            }
            uiService.GetUIController().UpdatePowerUpText(powerUpText);
            switch (powerUpType)
            {
                case PowerUpType.HealthPick:
                    playerService.GetPlayerController().IncreaseHealth((int)powerUpValue); // Increase health
                    yield return new WaitForSeconds(powerUpDuration);
                    break;
                case PowerUpType.HomingOrbs:
                    playerService.GetPlayerController().isHoming = true; // Activate homing bullets
                    yield return new WaitForSeconds(powerUpDuration);
                    playerService.GetPlayerController().isHoming = false; // Deactivate homing bullets
                    break;
                case PowerUpType.RapidFire:
                    playerService.GetPlayerController().shootCooldown /= powerUpValue; // Increase fire rate
                    yield return new WaitForSeconds(powerUpDuration);
                    playerService.GetPlayerController().shootCooldown *= powerUpValue; // Reset fire rate
                    break;
                case PowerUpType.Shield:
                    playerService.GetPlayerController().isShieldActive = true; // Activate shield
                    yield return new WaitForSeconds(powerUpDuration);
                    playerService.GetPlayerController().isShieldActive = false; // Deactivate shield
                    break;
                case PowerUpType.SlowMotion:
                    Time.timeScale = powerUpValue; // Slow down time
                    yield return new WaitForSeconds(powerUpDuration);
                    Time.timeScale = 1f; // Reset time
                    break;
                case PowerUpType.Teleport:
                    playerService.GetPlayerController().Teleport(powerUpValue); // Teleport player
                    yield return new WaitForSeconds(powerUpDuration);
                    break;
                default:
                    yield return new WaitForSeconds(1);
                    break;
            }
            uiService.GetUIController().UpdatePowerUpText(null); // Clear power-up text
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerController playerController = collider.GetComponent<PlayerController>();
                ActivatePowerUp(powerUpType, powerUpDuration, powerUpValue); // Activate power-up effect
                soundService.PlaySoundEffect(SoundType.PowerUpPickup);
                Destroy(gameObject); // Destroy the power-up after activation
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject); // Destroy the power-up if it goes off screen
        }
    }
}