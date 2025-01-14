using ServiceLocator.Actor;

namespace ServiceLocator.Bullet
{
    public class BulletModel
    {
        public BulletModel(BulletData _bulletData, ActorType _bulletOwnerActor, bool _isHoming)
        {
            BulletOwnerActor = _bulletOwnerActor;
            IsHoming = _isHoming;
            HomingSpeed = _bulletData.homingSpeed;
            HitScore = _bulletData.hitScore;
        }
        // Getters & Setters
        public ActorType BulletOwnerActor { get; private set; } // Tag of the bullet's owner
        public bool IsHoming { get; private set; } // Whether the bullet is homing or not
        public float HomingSpeed { get; private set; } // Speed of homing
        public int HitScore { get; private set; } // Value for score increment
    }
}