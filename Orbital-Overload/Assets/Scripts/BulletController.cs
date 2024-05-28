using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float bulletSpeed;
    private Vector2 bulletDirection = Vector2.zero;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        ShootBullet();
    }
    public void SetBullet(Vector2 direction, float speed)
    {
        bulletDirection = direction.normalized;
        bulletSpeed = speed;
    }
    private void ShootBullet()
    {
        rb.velocity = bulletDirection * bulletSpeed * Time.fixedDeltaTime;
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
