using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 1f;
    private float moveX = 0f;
    private float moveY = 0f;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float bulletSpeed = 1f;
    public float shootCooldown = 0.1f;
    private float lastShootTime = 0f;
    private Vector2 mouseDirection = Vector2.zero;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        MovementTowardsPlayer();
        RotateInput();
    }
    private void FixedUpdate()
    {
        Move();
        Shoot();
        Rotate();
    }
    private void MovementTowardsPlayer()
    {
        if (player != null)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            moveX = direction.x;
            moveY = direction.y;
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
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (bulletController != null)
            {
                bulletController.ShootBullet(shootPoint.up, bulletSpeed);
            }
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