using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class PowerUpController : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer powerUpSprite;

        // Private Variables
        public PowerUpData powerUpData;

        public void Init(PowerUpData _powerUpData)
        {
            // Setting Variables
            powerUpData = _powerUpData;
            powerUpSprite.color = _powerUpData.powerUpColor;

            Destroy(gameObject, powerUpData.powerUpLifetime); // Destroy the power-up after its lifetime
        }

        private void OnTriggerEnter2D(Collider2D _collider)
        {
            if (_collider.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject); // Destroy the power-up if it goes off screen
        }
    }
}