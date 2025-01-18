using ServiceLocator.Actor;
using ServiceLocator.Event;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class ShieldPowerUpController : PowerUpController
    {
        public ShieldPowerUpController(PowerUpData _powerUpData, PowerUpView _powerUpPrefab,
            Transform _powerUpParentPanel, Vector2 _spawnPosition,
            EventService _eventService) :

            base(_powerUpData, _powerUpPrefab,
            _powerUpParentPanel, _spawnPosition,
            _eventService)
        { }

        protected override void EnablePowerUp(ActorController _actorController)
        {
            _actorController.GetActorModel().IsShieldActive = true; // Activate shield
        }
        protected override void DisablePowerUp(ActorController _actorController)
        {
            _actorController.GetActorModel().IsShieldActive = false; // Deactivate shield
        }
    }
}