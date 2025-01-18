using ServiceLocator.Actor;
using ServiceLocator.Event;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class SlowMotionPowerUpController : PowerUpController
    {
        // Private Variables
        private float defaultFixedDeltaTime;

        public SlowMotionPowerUpController(PowerUpData _powerUpData, PowerUpView _powerUpPrefab,
            Transform _powerUpParentPanel, Vector2 _spawnPosition,
            EventService _eventService) :

            base(_powerUpData, _powerUpPrefab,
            _powerUpParentPanel, _spawnPosition,
            _eventService)
        {
            // Setting Variables
            defaultFixedDeltaTime = Time.fixedDeltaTime;
        }

        protected override void EnablePowerUp(ActorController _actorController)
        {
            Time.timeScale = Mathf.Lerp(1f, powerUpModel.PowerUpValue, powerUpModel.PowerUpDuration);
            Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;
        }
        protected override void DisablePowerUp(ActorController _actorController)
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = defaultFixedDeltaTime * 1f;
        }
    }
}