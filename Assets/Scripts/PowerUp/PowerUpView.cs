using ServiceLocator.Actor;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class PowerUpView : MonoBehaviour
    {
        [SerializeField] public SpriteRenderer powerUpSprite;
        [SerializeField] private Animator powerUpAnimator; // PowerUp Animator

        // Private Variables
        private PowerUpController powerUpController;
        private static readonly int IDLE_HASH = Animator.StringToHash("Idle");

        public void Init(PowerUpController _powerUpController)
        {
            // Setting Variables
            powerUpController = _powerUpController;
            Reset();
        }

        public void Reset()
        {
            powerUpSprite.sprite = powerUpController.GetPowerUpModel().PowerUpSprite;
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

        public void IdleAnimation()
        {
            powerUpAnimator.Play(IDLE_HASH, 0, 0f);
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
                    powerUpController.PlayVFX();
                }
            }
        }
    }
}