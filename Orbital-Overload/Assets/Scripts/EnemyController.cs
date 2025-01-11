using ServiceLocator.Bullet;
using ServiceLocator.Player;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f; // Speed of enemy movement
    [SerializeField] private GameObject bulletPrefab; // Bullet prefab for shooting
    [SerializeField] private Transform shootPoint; // Point from where bullets are shot
    [SerializeField] private float shootSpeed = 1f; // Speed of bullets
    [SerializeField] private float awayFromPlayerDistance = 1f; // Minimum distance to keep from player
    [SerializeField] private float shootCooldown = 0.1f; // Cooldown between shots

    private float moveX = 0f; // X-axis movement
    private float moveY = 0f; // Y-axis movement
    private float lastShootTime = 0f; // Time of last shot
    private Vector2 mouseDirection = Vector2.zero; // Direction towards the player
    private GameObject player; // Reference to the player

    // Private Services
    private BulletService bulletService;

    public void Init(BulletService _bulletService)
    {
        // Setting Variables
        player = GameObject.FindGameObjectWithTag("Player");

        // Setting Services
        bulletService = _bulletService;
    }

    private void Update()
    {
        MoveTowardsPlayerDirection(); // Calculate movement direction towards player
        RotateInput(); // Update rotation direction
    }

    private void FixedUpdate()
    {
        Move(); // Move enemy
        Shoot(); // Shoot bullets
        Rotate(); // Rotate enemy towards player
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); // Destroy enemy when it goes off screen
    }

    private void MoveTowardsPlayerDirection()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (player != null && distanceToPlayer > awayFromPlayerDistance)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            moveX = direction.x;
            moveY = direction.y;
        }
        else
        {
            moveX = 0.0f;
            moveY = 0.0f;
        }
    }

    private void Move()
    {
        Vector2 moveVector = new Vector2(moveX, moveY) * moveSpeed * Time.fixedDeltaTime;
        transform.Translate(moveVector, Space.World);
    }

    private void Shoot()
    {
        if (Time.time >= lastShootTime + shootCooldown)
        {
            lastShootTime = Time.time;
            bulletService.Shoot(gameObject.tag, shootPoint, shootSpeed, false);
        }
    }

    private void RotateInput()
    {
        if (player != null)
        {
            mouseDirection = (player.transform.position - transform.position).normalized;
        }
    }

    private void Rotate()
    {
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}