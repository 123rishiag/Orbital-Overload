using ServiceLocator.Player;
using ServiceLocator.Sound;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private PowerUpType powerUpType; // Type of power-up
    [SerializeField] private float powerUpDuration = 5f; // Duration of the power-up effect
    [SerializeField] private float powerUpValue = 0; // Value of the power-up effect
    [SerializeField] private float powerUpLifetime = 10f; // Lifetime of the power-up before it expires

    // Private Services
    private SoundService soundService;

    private void Start()
    {
        Destroy(gameObject, powerUpLifetime); // Destroy the power-up after its lifetime
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerController playerController = collider.GetComponent<PlayerController>();
            playerController.ActivatePowerUp(powerUpType, powerUpDuration, powerUpValue); // Activate power-up effect
            soundService.PlaySoundEffect(SoundType.PowerUpPickup);
            Destroy(gameObject); // Destroy the power-up after activation
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); // Destroy the power-up if it goes off screen
    }
}

public enum PowerUpType
{
    HealthPick,
    HomingOrbs,
    RapidFire,
    Shield,
    SlowMotion,
    Teleport
}