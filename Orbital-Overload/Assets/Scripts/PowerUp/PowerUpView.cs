using ServiceLocator.Actor;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class PowerUpView : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer powerUpSprite;

        // Private Variables
        private PowerUpController powerUpController;

        public void Init(PowerUpController _powerUpController)
        {
            // Setting Variables
            powerUpController = _powerUpController;
            Reset();
        }

        public void Reset()
        {
            powerUpSprite.color = powerUpController.GetPowerUpModel().PowerUpColor;
            Invoke(nameof(HideView), powerUpController.GetPowerUpModel().PowerUpLifetime); // HideView after the lifetime
        }

        public void SetPosition(Vector2 _position)
        {
            transform.position = _position;
        }

        public void ShowView()
        {
            gameObject.SetActive(true);
        }

        public void HideView()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D _collider)
        {
            if (_collider.CompareTag("Actor"))
            {
                ActorView actorView = _collider.GetComponent<ActorView>();
                if (actorView.actorController.GetActorModel().ActorType == ActorType.Player)
                {
                    powerUpController.ActivatePowerUp(actorView.actorController);
                    HideView();
                }
            }
        }
    }
}