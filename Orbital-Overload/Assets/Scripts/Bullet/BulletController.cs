using ServiceLocator.Actor;
using ServiceLocator.Sound;
using UnityEngine;

namespace ServiceLocator.Bullet
{
    public class BulletController
    {
        // Private Variables
        private BulletModel bulletModel;
        private BulletView bulletView;
        public ActorView enemy; // Target enemy for homing bullets

        // Private Services
        private SoundService soundService;
        private ActorService actorService;

        public BulletController(BulletConfig _bulletConfig,
            ActorType _bulletOwnerActor, float _shootSpeed, bool _isHoming, Transform _shootPoint,
            SoundService _soundService, ActorService _actorService)
        {
            bulletModel = new BulletModel(_bulletConfig.bulletData, _bulletOwnerActor, _isHoming);
            bulletView = Object.Instantiate(_bulletConfig.bulletPrefab, _shootPoint.position, _shootPoint.rotation).
                GetComponent<BulletView>();
            bulletView.Init(this);

            // Setting Services
            soundService = _soundService;
            actorService = _actorService;

            // Setting Elements
            ShootBullet(_shootPoint, _shootSpeed); // Shoot the bullet
        }

        public void Update()
        {
            FindNearestEnemy(); // Find the nearest enemy for homing
        }

        public void FixedUpdate()
        {
            Homing(); // Homing logic in FixedUpdate for physics
        }

        private void FindNearestEnemy()
        {
            if (bulletModel.IsHoming && enemy == null)
            {
                ActorView nearestActor = null;
                float minDistance = Mathf.Infinity;
                Vector2 currentPosition = bulletView.transform.position;

                // Find the nearest enemy
                foreach (var actorController in actorService.GetEnemyActorControllers())
                {
                    // Avoid Hitting Player
                    if (actorController.GetActorModel().ActorType == ActorType.Player) continue;

                    // Avoid Dead Enemies
                    if (!actorController.IsAlive()) return;

                    // Fetching Distance from enemies
                    float distance = Vector2.Distance(actorController.GetActorView().transform.position, currentPosition);
                    if (distance < minDistance)
                    {
                        nearestActor = actorController.GetActorView();
                        minDistance = distance;
                    }
                }
                if (nearestActor != null)
                {
                    enemy = nearestActor.GetComponent<ActorView>();
                }
            }
        }

        private void Homing()
        {
            if (bulletModel.IsHoming && enemy != null)
            {
                Vector2 enemyDirection = ((Vector2)enemy.transform.position - bulletView.rigidBody.position).normalized;
                bulletView.rigidBody.velocity =
                    Vector2.Lerp(bulletView.rigidBody.velocity, enemyDirection * bulletModel.HomingSpeed, Time.fixedDeltaTime);
            }
        }

        public void ShootBullet(Transform _shootPoint, float _bulletSpeed)
        {
            bulletView.rigidBody.velocity = _shootPoint.up * _bulletSpeed * Time.fixedDeltaTime; // Set bullet velocity
            soundService.PlaySoundEffect(SoundType.BulletShoot);
        }

        // Getters
        public BulletModel GetBulletModel() => bulletModel;
        public BulletView GetBulletView() => bulletView;
    }
}