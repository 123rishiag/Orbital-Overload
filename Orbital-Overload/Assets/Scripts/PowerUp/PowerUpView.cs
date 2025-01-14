using ServiceLocator.Actor;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class PowerUpView : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer powerUpSprite;

        // Private Variables
        private PowerUpController powerUpController;

        public void Init(PowerUpController _powerUpController, Color _powerUpColor)
        {
            // Setting Variables
            powerUpController = _powerUpController;
            powerUpSprite.color = _powerUpColor;

            Destroy(gameObject, powerUpController.GetPowerUpModel().PowerUpLifetime); // Destroy the power-up after its lifetime
        }

        private void OnTriggerEnter2D(Collider2D _collider)
        {
            if (_collider.CompareTag("Actor"))
            {
                ActorView actorView = _collider.GetComponent<ActorView>();
                if (actorView.actorController.GetActorModel().ActorType == ActorType.Player)
                {
                    powerUpController.ActivatePowerUp(actorView.actorController);
                    Destroy(gameObject);
                }
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject); // Destroy the power-up if it goes off screen
        }
    }
}