using ServiceLocator.Actor;

namespace ServiceLocator.Projectile
{
    public class ProjectileModel
    {
        public ProjectileModel(ProjectileData _projectileData, ActorType _projectileOwnerActor, bool _isHoming)
        {
            ProjectileOwnerActor = _projectileOwnerActor;
            IsHoming = _isHoming;
            HomingSpeed = _projectileData.homingSpeed;
            HitScore = _projectileData.hitScore;
        }
        // Getters & Setters
        public ActorType ProjectileOwnerActor { get; private set; } // Tag of the projectile's owner
        public bool IsHoming { get; private set; } // Whether the projectile is homing or not
        public float HomingSpeed { get; private set; } // Speed of homing
        public int HitScore { get; private set; } // Value for score increment
    }
}