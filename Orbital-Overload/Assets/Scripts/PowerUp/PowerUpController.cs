using ServiceLocator.Player;
using ServiceLocator.Sound;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class PowerUpController : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer powerUpSprite;

        // Private Variables
        private PowerUpData powerUpData;

        // Private Services
        private SoundService soundService;
        private PowerUpService powerUpService;

        public void Init(PowerUpData _powerUpData, SoundService _soundService, PowerUpService _powerUpService)
        {
            // Setting Variables
            powerUpData = _powerUpData;
            powerUpSprite.color = _powerUpData.powerUpColor;

            // Setting Services
            soundService = _soundService;
            powerUpService = _powerUpService;

            GameObject.Destroy(gameObject, powerUpData.powerUpLifetime); // Destroy the power-up after its lifetime
        }



        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerController playerController = collider.GetComponent<PlayerController>();
                powerUpService.ActivatePowerUp(powerUpData); // Activate power-up effect
                GameObject.Destroy(gameObject); // Some additional time or coroutine to work
                soundService.PlaySoundEffect(SoundType.PowerUpPickup);
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject); // Destroy the power-up if it goes off screen
        }
    }
}