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
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();

        }
        else if(collider.CompareTag("Enemy"))
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Bullet"))
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }
}
