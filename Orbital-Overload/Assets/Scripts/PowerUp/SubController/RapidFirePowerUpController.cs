using ServiceLocator.Actor;
using ServiceLocator.Event;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class RapidFirePowerUpController : PowerUpController
    {
        public RapidFirePowerUpController(PowerUpData _powerUpData, PowerUpView _powerUpPrefab,
            Transform _powerUpParentPanel, Vector2 _spawnPosition,
            EventService _eventService) :

            base(_powerUpData, _powerUpPrefab,
            _powerUpParentPanel, _spawnPosition,
            _eventService)
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