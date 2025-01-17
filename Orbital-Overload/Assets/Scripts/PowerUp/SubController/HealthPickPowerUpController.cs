using ServiceLocator.Actor;
using ServiceLocator.Main;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class HealthPickPowerUpController : PowerUpController
    {
        public HealthPickPowerUpController(PowerUpData _powerUpData, PowerUpView _powerUpPrefab,
            Transform _powerUpParentPanel, Vector2 _spawnPosition,
            GameService _gameService, SoundService _soundService, UIService _uiService):

            base(_powerUpData, _powerUpPrefab,
            _powerUpParentPanel, _spawnPosition,
            _gameService, _soundService, _uiService)
        { }

        protected override void EnablePowerUp(ActorController _actorController)
        {
            _actorController.IncreaseHealth((int)powerUpModel.PowerUpValue); // Increase health
        }
        protected override void DisablePowerUp(ActorController _actorController) { }
    }
}