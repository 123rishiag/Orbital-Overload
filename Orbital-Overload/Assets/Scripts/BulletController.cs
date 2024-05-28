using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void ShootBullet(Vector2 bulletDirection, float bulletSpeed)
    {
        rb.velocity = bulletDirection * bulletSpeed * Time.fixedDeltaTime;
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
