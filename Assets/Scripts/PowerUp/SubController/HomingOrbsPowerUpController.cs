using ServiceLocator.Actor;
using ServiceLocator.Event;
using ServiceLocator.Projectile;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    public class HomingOrbsPowerUpController : PowerUpController
    {
        public HomingOrbsPowerUpController(PowerUpData _powerUpData, PowerUpView _powerUpPrefab,
            Transform _powerUpParentPanel, Vector2 _spawnPosition,
            EventService _eventService) :

            base(_powerUpData, _powerUpPrefab,
            _powerUpParentPanel, _spawnPosition,
            _eventService)
        { }

        protected override void EnablePowerUp(ActorController _actorController)
        {
            _actorController.GetActorModel().ProjectileType = ProjectileType.Homing_Bullet; // Activate homing projectiles
        }
        protected override void DisablePowerUp(ActorController _actorController)
        {
            _actorController.GetActorModel().ProjectileType = ProjectileType.Normal_Bullet; // Deactivate homing projectiles
        }
    }
}