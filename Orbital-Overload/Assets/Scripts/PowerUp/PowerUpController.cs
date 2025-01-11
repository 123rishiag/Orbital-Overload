using ServiceLocator.Player;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using System.Collections;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class PowerUpController : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer powerUpSprite;
        [SerializeField] public CircleCollider2D powerUpCollider;

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
            powerUpSprite.color = _powerUpData.powerUpColor;
            powerUpDuration = _powerUpData.powerUpDuration;
            powerUpValue = _powerUpData.powerUpValue;
            powerUpLifetime = _powerUpData.powerUpLifetime;

            // Setting Services
            soundService = _soundService;
            uiService = _uiService;
            playerService = _playerService;
            
            GameObject.Destroy(gameObject, powerUpLifetime); // Destroy the power-up after its lifetime
        }

        private void ActivatePowerUp()
        {
            StartCoroutine(PowerUp()); // Activate power-up effect
        }

        private IEnumerator PowerUp()
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
                    yield return new WaitForSecondsRealtime(powerUpDuration);
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
            uiService.GetUIController().HidePowerUpText(); // Hide power-up text
            GameObject.Destroy(gameObject); // Some additional time or coroutine to work
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                // Hiding Powerup and disabling collider on collison
                Color newColor = powerUpSprite.color;
                newColor.a = 0;
                powerUpSprite.color = newColor;
                powerUpCollider.enabled = false;

                PlayerController playerController = collider.GetComponent<PlayerController>();
                ActivatePowerUp(); // Activate power-up effect
                soundService.PlaySoundEffect(SoundType.PowerUpPickup);
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject); // Destroy the power-up if it goes off screen
        }
    }
}