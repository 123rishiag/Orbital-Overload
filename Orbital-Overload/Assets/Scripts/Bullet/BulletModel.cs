namespace ServiceLocator.Bullet
{
    public class BulletModel
    {
        public BulletModel(BulletData _bulletData, string _bulletOwnerTag, bool _isHoming)
        {
            BulletOwnerTag = _bulletOwnerTag;
            IsHoming = _isHoming;
            HomingSpeed = _bulletData.homingSpeed;
        }
        // Getters & Setters
        public string BulletOwnerTag { get; private set; } // Tag of the bullet's owner
        public bool IsHoming { get; private set; } // Whether the bullet is homing or not
        public float HomingSpeed { get; private set; } // Speed of homing
    }
}