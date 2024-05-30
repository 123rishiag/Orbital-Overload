using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject enemy;
    private bool isHoming = false;
    private float homingSpeed = 0f;
    private Vector2 enemyDirection = Vector2.zero;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        FindNearestEnemy();
    }
    private void FixedUpdate()
    {
        Homing();
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Destroy(gameObject);
            PlayerController playerController = collider.GetComponent<PlayerController>();
            if (playerController.isShieldActive != true)
            {
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();
            }
        }
        else if (collider.CompareTag("Enemy"))
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
        rb.velocity = bulletDirection * bulletSpeed * Time.fixedDeltaTime;
    }
    
    public void SetHoming(bool _isHoming, float _homingSpeed)
    {
        isHoming = _isHoming;
        homingSpeed = _homingSpeed;
    }
}
