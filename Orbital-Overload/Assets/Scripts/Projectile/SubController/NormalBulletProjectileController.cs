using ServiceLocator.Actor;
using ServiceLocator.Sound;
using UnityEngine;

namespace ServiceLocator.Projectile
{
    public class NormalBulletProjectileController : ProjectileController
    {
        public NormalBulletProjectileController(ProjectileConfig _projectileConfig,
            ActorType _projectileOwnerActor, float _shootSpeed, Transform _shootPoint, int _projectileIndex,
            SoundService _soundService, ActorService _actorService) :
            base(_projectileConfig,
            _projectileOwnerActor, _shootSpeed, _shootPoint, _projectileIndex,
            _soundService, _actorService)
        { }
    }
}