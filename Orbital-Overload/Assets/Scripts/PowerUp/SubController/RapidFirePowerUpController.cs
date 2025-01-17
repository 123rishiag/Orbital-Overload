using ServiceLocator.Actor;
using ServiceLocator.Main;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class RapidFirePowerUpController : PowerUpController
    {
        public RapidFirePowerUpController(PowerUpData _powerUpData, PowerUpView _powerUpPrefab,
            Transform _powerUpParentPanel, Vector2 _spawnPosition,
            GameService _gameService, SoundService _soundService, UIService _uiService) :

            base(_powerUpData, _powerUpPrefab,
            _powerUpParentPanel, _spawnPosition,
            _gameService, _soundService, _uiService)
        { }

        protected override void EnablePowerUp(ActorController _actorController)
        {
            _actorController.GetActorModel().ShootCooldown /= powerUpModel.PowerUpValue; // Increase fire rate
        }
        protected override void DisablePowerUp(ActorController _actorController)
        {
            _actorController.GetActorModel().ShootCooldown *= powerUpModel.PowerUpValue; // Reset fire rate
        }
    }
}