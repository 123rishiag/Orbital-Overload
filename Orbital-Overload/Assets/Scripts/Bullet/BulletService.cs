using ServiceLocator.Actor;
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
        private ActorService actorService;

        public BulletService(BulletConfig _bulletConfig)
        {
            // Setting Variables
            bulletConfig = _bulletConfig;
            bullets = new List<BulletController>();
        }

        public void Init(SoundService _soundService, ActorService _actorService)
        {
            // Setting Services
            soundService = _soundService;
            actorService = _actorService;
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

        public void Shoot(ActorType _bulletOwnerActor, float _shootSpeed, bool _isHoming, Transform _shootPoint)
        {
            BulletController bulletController =
                new BulletController(bulletConfig, _bulletOwnerActor, _shootSpeed, _isHoming, _shootPoint,
                soundService, actorService);
            bullets.Add(bulletController);
        }
    }
}