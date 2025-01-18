using ServiceLocator.Actor;
using ServiceLocator.Event;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class SlowMotionPowerUpController : PowerUpController
    {
        public SlowMotionPowerUpController(PowerUpData _powerUpData, PowerUpView _powerUpPrefab,
            Transform _powerUpParentPanel, Vector2 _spawnPosition,
            EventService _eventService) :

            base(_powerUpData, _powerUpPrefab,
            _powerUpParentPanel, _spawnPosition,
            _eventService)
        { }

        protected override void EnablePowerUp(ActorController _actorController)
        {
            Time.timeScale = powerUpModel.PowerUpValue; // Slow down time
        }
        protected override void DisablePowerUp(ActorController _actorController)
        {
            Time.timeScale = 1f; // Reset time
        }
    }
}