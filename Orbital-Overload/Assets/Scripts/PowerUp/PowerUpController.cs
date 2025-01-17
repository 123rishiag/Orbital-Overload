using ServiceLocator.Actor;
using ServiceLocator.Main;
using ServiceLocator.Projectile;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using System.Collections;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class PowerUpController
    {
        // Private Variables
        private PowerUpModel powerUpModel;
        private PowerUpView powerUpView;

        // Private Services
        private GameService gameService;
        private SoundService soundService;
        private UIService uiService;

        public PowerUpController(PowerUpData _powerUpData, PowerUpView _powerUpPrefab,
            Transform _powerUpParentPanel, Vector2 _spawnPosition,
            GameService _gameService, SoundService _soundService, UIService _uiService)
        {
            // Setting Variables
            powerUpModel = new PowerUpModel(_powerUpData);
            powerUpView = Object.Instantiate(_powerUpPrefab, _spawnPosition, Quaternion.identity, _powerUpParentPanel).
                GetComponent<PowerUpView>();
            powerUpView.Init(this);

            // Setting Services
            gameService = _gameService;
            soundService = _soundService;
            uiService = _uiService;
        }

        public void Reset(PowerUpData _powerUpData, Vector2 _spawnPosition)
        {
            powerUpModel.Reset(_powerUpData);
            powerUpView.Reset();
            powerUpView.SetPosition(_spawnPosition);
            powerUpView.ShowView();
        }

        public void ActivatePowerUp(ActorController _actorController)
        {
            gameService.StartManagedCoroutine(PowerUp(_actorController)); // Activate power-up effect
        }

        private IEnumerator PowerUp(ActorController _actorController)
        {
            string powerUpText;
            if (powerUpModel.PowerUpType == PowerUpType.HealthPick || powerUpModel.PowerUpType == PowerUpType.Teleport)
            {
                powerUpText = powerUpModel.PowerUpType.ToString() + "ed.";
            }
            else
            {
                powerUpText = powerUpModel.PowerUpType.ToString() + " activated for " + powerUpModel.PowerUpDuration.ToString() + " seconds.";
            }
            soundService.PlaySoundEffect(SoundType.PowerUpPickup);
            uiService.GetUIController().GetUIView().UpdatePowerUpText(powerUpText);
            switch (powerUpModel.PowerUpType)
            {
                case PowerUpType.HealthPick:
                    _actorController.IncreaseHealth((int)powerUpModel.PowerUpValue); // Increase health
                    yield return new WaitForSeconds(powerUpModel.PowerUpDuration);
                    break;
                case PowerUpType.HomingOrbs:
                    _actorController.GetActorModel().ProjectileType = ProjectileType.Homing_Bullet; // Activate homing projectiles
                    yield return new WaitForSeconds(powerUpModel.PowerUpDuration);
                    _actorController.GetActorModel().ProjectileType = ProjectileType.Normal_Bullet; // Deactivate homing projectiles
                    break;
                case PowerUpType.RapidFire:
                    _actorController.GetActorModel().ShootCooldown /= powerUpModel.PowerUpValue; // Increase fire rate
                    yield return new WaitForSeconds(powerUpModel.PowerUpDuration);
                    _actorController.GetActorModel().ShootCooldown *= powerUpModel.PowerUpValue; // Reset fire rate
                    break;
                case PowerUpType.Shield:
                    _actorController.GetActorModel().IsShieldActive = true; // Activate shield
                    yield return new WaitForSeconds(powerUpModel.PowerUpDuration);
                    _actorController.GetActorModel().IsShieldActive = false; // Deactivate shield
                    break;
                case PowerUpType.SlowMotion:
                    Time.timeScale = powerUpModel.PowerUpValue; // Slow down time
                    yield return new WaitForSecondsRealtime(powerUpModel.PowerUpDuration);
                    Time.timeScale = 1f; // Reset time
                    break;
                case PowerUpType.Teleport:
                    _actorController.Teleport(powerUpModel.PowerUpValue); // Teleport player
                    yield return new WaitForSeconds(powerUpModel.PowerUpDuration);
                    break;
                default:
                    yield return new WaitForSeconds(1);
                    break;
            }
            uiService.GetUIController().GetUIView().HidePowerUpText(); // Hide power-up text
        }

        public bool IsActive()
        {
            if (!powerUpView.gameObject.activeInHierarchy) return false;
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(powerUpView.transform.position);
            if (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1)
            {
                return false;
            }
            return true;
        }

        // Getters
        public PowerUpModel GetPowerUpModel() => powerUpModel;
        public PowerUpView GetPowerUpView() => powerUpView;
    }
}