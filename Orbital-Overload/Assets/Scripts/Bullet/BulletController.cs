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

        public BulletController(BulletConfig _bulletConfig,
            ActorType _bulletOwnerActor, float _shootSpeed, bool _isHoming, Transform _shootPoint,
            SoundService _soundService)
        {
            bulletModel = new BulletModel(_bulletConfig.bulletData, _bulletOwnerActor, _isHoming);
            bulletView = Object.Instantiate(_bulletConfig.bulletPrefab, _shootPoint.position, _shootPoint.rotation).
                GetComponent<BulletView>();
            bulletView.Init(this);

            // Setting Services
            soundService = _soundService;

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
                GameObject[] actors = GameObject.FindGameObjectsWithTag("Actor");
                GameObject nearestActor = null;
                float minDistance = Mathf.Infinity;
                Vector2 currentPosition = bulletView.transform.position;

                // Find the nearest enemy
                foreach (GameObject actor in actors)
                {
                    // Avoid Hitting Player
                    if (actor.GetComponent<ActorView>().actorController.GetActorModel().ActorType == ActorType.Player) continue;

                    float distance = Vector2.Distance(actor.transform.position, currentPosition);
                    if (distance < minDistance)
                    {
                        nearestActor = actor;
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