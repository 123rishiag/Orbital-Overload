using ServiceLocator.Sound;
using UnityEngine;

namespace ServiceLocator.Bullet
{
    public class BulletService
    {
        // Private Variables
        private BulletConfig bulletConfig;

        // Private Services
        private SoundService soundService;

        public BulletService(BulletConfig _bulletConfig, SoundService _soundService)
        {
            // Setting Variables
            bulletConfig = _bulletConfig;

            // Setting Services
            soundService = _soundService;
        }

        public void Shoot(string _ownerTag, Transform _shootPoint, float _shootSpeed, bool _isHoming)
        {
            GameObject bullet = GameObject.Instantiate(bulletConfig.bulletData.bulletPrefab, _shootPoint.position, _shootPoint.rotation);
            BulletController bulletController = bullet.GetComponent<BulletController>();
            bulletController.Init(soundService);
            if (bulletController != null)
            {
                bulletController.SetOwnerTag(_ownerTag); // Set owner tag to avoid self-collision
                if (_isHoming)
                {
                    bulletController.SetHoming(_isHoming, bulletConfig.bulletData.homingSpeed); // Set homing properties
                }
                bulletController.ShootBullet(_shootPoint.up, _shootSpeed); // Shoot the bullet
            }
        }
    }
}