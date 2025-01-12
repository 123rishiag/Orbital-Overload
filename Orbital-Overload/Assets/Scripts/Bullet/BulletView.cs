using ServiceLocator.Player;
using ServiceLocator.Sound;
using UnityEngine;

namespace ServiceLocator.Bullet
{
    public class BulletView : MonoBehaviour
    {
        // Private Variables
        public Rigidbody2D rigidBody; // Rigidbody2D component of the bullet// Private Variables
        private BulletController bulletController;

        // Private Services
        private SoundService soundService;

        public void Init(BulletController _bulletController, SoundService _soundService)
        {
            // Setting Variables
            bulletController = _bulletController;
            rigidBody = GetComponent<Rigidbody2D>();

            // Setting Services
            soundService = _soundService;
        }

        private void OnTriggerEnter2D(Collider2D _collider)
        {
            // Avoid collision with the owner
            if (_collider.CompareTag(bulletController.GetBulletModel().BulletOwnerTag)) return;

            if (_collider.CompareTag("Player"))
            {
                PlayerController playerController = _collider.GetComponent<PlayerController>();
                if (!playerController.ShieldActive())
                {
                    playerController.DecreaseHealth(); // Decrease player's health on hit
                    soundService.PlaySoundEffect(SoundType.PlayerHurt);
                }
                Destroy(gameObject); // Destroy bullet on hit
            }
            else if (_collider.CompareTag("Enemy"))
            {
                PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                playerController.AddScore(); // Increase player score on hit
                Destroy(_collider.gameObject); // Destroy enemy on hit
                Destroy(gameObject); // Destroy bullet on hit
                soundService.PlaySoundEffect(SoundType.EnemyHurt);
            }
            else if (_collider.CompareTag("Bullet"))
            {
                Destroy(_collider.gameObject); // Destroy other bullet on collision
                Destroy(gameObject); // Destroy bullet on hit
                soundService.PlaySoundEffect(SoundType.BulletShoot);
            }
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject); // Destroy bullet when it goes off screen
        }
    }
}