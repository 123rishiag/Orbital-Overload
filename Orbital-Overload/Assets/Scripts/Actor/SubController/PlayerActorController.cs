using ServiceLocator.Control;
using ServiceLocator.Projectile;
using ServiceLocator.Sound;
using ServiceLocator.UI;
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
            SoundService _soundService, UIService _uiService, InputService _inputService,
            ProjectileService _projectileService, ActorService _actorService) :

            base(_actorData, _actorPrefab,
                _actorParentPanel, _spawnPosition,
                _soundService, _uiService, _inputService,
                _projectileService, _actorService)
        {
            // Setting Variables
            isShooting = false;
            playerCasualMoveSpeed = _playerCasualMoveSpeed;
        }
        public override void Update()
        {
            base.Update();

            uiService.GetUIController().GetUIView().UpdateHealthText(actorModel.CurrentHealth);
            uiService.GetUIController().GetUIView().UpdateScoreText(actorModel.CurrentScore);
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
    }
}