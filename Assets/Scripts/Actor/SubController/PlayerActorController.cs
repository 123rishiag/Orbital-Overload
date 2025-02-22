using ServiceLocator.Control;
using ServiceLocator.Event;
using ServiceLocator.Projectile;
using ServiceLocator.UI;
using ServiceLocator.Vision;
using UnityEngine;

namespace ServiceLocator.Actor
{
    public class PlayerActorController : ActorController
    {
        // Private Variables
        private float playerCasualMoveSpeed; // Default move speed when idle

        public PlayerActorController(ActorData _actorData, ActorView _actorPrefab,
            Transform _actorParentPanel, Vector2 _spawnPosition,
            float _playerCasualMoveSpeed,
            EventService _eventService, InputService _inputService,
            ProjectileService _projectileService, ActorService _actorService) :

            base(_actorData, _actorPrefab,
                _actorParentPanel, _spawnPosition,
                _eventService, _inputService,
                _projectileService, _actorService)
        {
            // Setting Variables
            isShooting = false;
            playerCasualMoveSpeed = _playerCasualMoveSpeed;
        }
        protected override void MovementInput()
        {
            moveX = inputService.GetPlayerMovement.x; // Get horizontal input
            moveY = inputService.GetPlayerMovement.y; // Get vertical input
            if (moveX == 0f)
            {
                moveX = playerCasualMoveSpeed; // Default move speed
            }
        }
        protected override void ShootInput()
        {
            if (inputService.IsPlayerShooting)
            {
                isShooting = true; // Start shooting
            }
            else if (!inputService.IsPlayerShooting)
            {
                isShooting = false; // Stop shooting
            }
        }
        protected override void RotateInput()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(inputService.GetMousePosition);
            mousePosition.z = 0f; // Ensure z is zero for 2D
            mouseDirection = (mousePosition - actorView.transform.position).normalized;
        }

        public override void DecreaseHealth()
        {
            if (!actorModel.IsShieldActive)
            {
                base.DecreaseHealth();
                eventService.OnDoShakeScreenEvent.Invoke(CameraShakeType.Normal);
                UpdateHealthUI();
            }
        }

        public override void IncreaseHealth(int _increaseHealth)
        {
            base.IncreaseHealth(_increaseHealth);
            UpdateHealthUI();
        }

        public override void UpdateHealthUI()
        {
            eventService.OnGetUIControllerEvent.Invoke<UIController>().GetUIView().UpdateHealthText(actorModel.CurrentHealth);
        }
        public override void UpdateScoreUI()
        {
            eventService.OnGetUIControllerEvent.Invoke<UIController>().GetUIView().UpdateScoreText(actorModel.CurrentScore);
        }
    }
}