using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb; // Rigidbody2D component of the bullet
    private GameObject enemy; // Target enemy for homing bullets
    private bool isHoming = false; // Whether the bullet is homing or not
    private float homingSpeed = 0f; // Speed of homing
    private Vector2 enemyDirection = Vector2.zero; // Direction towards the enemy
    private string bulletOwnerTag; // Tag of the bullet's owner

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        FindNearestEnemy(); // Find the nearest enemy for homing
    }

    private void FixedUpdate()
    {
        Homing(); // Homing logic in FixedUpdate for physics
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); // Destroy bullet when it goes off screen
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Avoid collision with the owner
        if (collider.CompareTag(bulletOwnerTag)) return;

        if (collider.CompareTag("Player"))
        {
            PlayerController playerController = collider.GetComponent<PlayerController>();
            if (!playerController.ShieldActive())
            {
                playerController.DecreaseHealth(); // Decrease player's health on hit
                SoundManager.Instance.PlayEffect(SoundType.PlayerHurt);
            }
            Destroy(gameObject); // Destroy bullet on hit
        }
        else if (collider.CompareTag("Enemy"))
        {
            PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            playerController.AddScore(); // Increase player score on hit
            SoundManager.Instance.PlayEffect(SoundType.PlayerHurt);
            Destroy(collider.gameObject); // Destroy enemy on hit
            Destroy(gameObject); // Destroy bullet on hit
        }
        else if (collider.CompareTag("Bullet"))
        {
            SoundManager.Instance.PlayEffect(SoundType.BulletShoot);
            Destroy(collider.gameObject); // Destroy other bullet on collision
            Destroy(gameObject); // Destroy bullet on hit
        }
    }

    private void Homing()
    {
        if (isHoming && enemy != null)
        {
            enemyDirection = ((Vector2)enemy.transform.position - rb.position).normalized;
            rb.velocity = Vector2.Lerp(rb.velocity, enemyDirection * homingSpeed, Time.fixedDeltaTime);
        }
    }

    private void FindNearestEnemy()
    {
        if (isHoming && enemy == null)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject nearestEnemy = null;
            float minDistance = Mathf.Infinity;
            Vector2 currentPosition = transform.position;

            // Find the nearest enemy
            foreach (GameObject enemy in enemies)
            {
                float distance = Vector2.Distance(enemy.transform.position, currentPosition);
                if (distance < minDistance)
                {
                    nearestEnemy = enemy;
                    minDistance = distance;
                }
            }
            enemy = nearestEnemy;
        }
    }

    public void ShootBullet(Vector2 bulletDirection, float bulletSpeed)
    {
        rb.velocity = bulletDirection * bulletSpeed * Time.fixedDeltaTime; // Set bullet velocity
        SoundManager.Instance.PlayEffect(SoundType.BulletShoot);
    }

    public void SetHoming(bool _isHoming, float _homingSpeed)
    {
        isHoming = _isHoming;
        homingSpeed = _homingSpeed;
    }

    public void SetOwnerTag(string ownerTag)
    {
        bulletOwnerTag = ownerTag;
    }
}