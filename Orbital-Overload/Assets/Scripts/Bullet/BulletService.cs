using ServiceLocator.Sound;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Bullet
{
    public class BulletService
    {
        // Private Variables
        private BulletConfig bulletConfig;
        private List<BulletController> bullets;

        // Private Services
        private SoundService soundService;

        public BulletService(BulletConfig _bulletConfig, SoundService _soundService)
        {
            // Setting Variables
            bulletConfig = _bulletConfig;
            bullets = new List<BulletController>();

            // Setting Services
            soundService = _soundService;
        }

        public void Update()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                BulletController bullet = bullets[i];
                if (bullet.GetBulletView() != null)
                {
                    bullet.Update();
                }
                else
                {
                    bullets.RemoveAt(i); // Safely remove without affecting iteration
                }
            }
        }

        public void FixedUpdate()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                BulletController bullet = bullets[i];
                if (bullet.GetBulletView() != null)
                {
                    bullet.FixedUpdate();
                }
            }
        }

        public void Shoot(string _ownerTag, float _shootSpeed, bool _isHoming, Transform _shootPoint)
        {
            BulletController bulletController =
                new BulletController(bulletConfig.bulletData, _ownerTag, _shootSpeed, _isHoming, _shootPoint, soundService);
            bullets.Add(bulletController);
        }
    }
}