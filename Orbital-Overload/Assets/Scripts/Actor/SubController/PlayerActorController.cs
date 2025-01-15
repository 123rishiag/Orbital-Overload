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

        public PlayerActorController(ActorConfig _actorConfig, Vector2 _spawnPosition, int _actorIndex,
            SoundService _soundService, UIService _uiService, ProjectileService _projectileService, ActorService _actorService) :
            base(_actorConfig.playerData, _actorConfig.actorPrefab, _spawnPosition,
                _soundService, _uiService, _projectileService, _actorService)
        {
            // Setting Variables
            isShooting = false;
            playerCasualMoveSpeed = _actorConfig.playerCasualMoveSpeed;
        }
        public override void Update()
        {
            base.Update();

            uiService.GetUIController().GetUIView().UpdateHealthText(actorModel.CurrentHealth);
            uiService.GetUIController().GetUIView().UpdateScoreText(actorModel.CurrentScore);
        }
        protected override void MovementInput()
        {
            moveX = Input.GetAxis("Horizontal"); // Get horizontal input
            moveY = Input.GetAxis("Vertical"); // Get vertical input
            if (moveX == 0f)
            {
                moveX = playerCasualMoveSpeed; // Default move speed
            }
        }
        protected override void ShootInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isShooting = true; // Start shooting
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isShooting = false; // Stop shooting
            }
        }
        protected override void RotateInput()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Ensure z is zero for 2D
            mouseDirection = (mousePosition - actorView.transform.position).normalized;
        }
    }
}