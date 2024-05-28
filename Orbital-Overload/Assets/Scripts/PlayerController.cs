using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    private float moveX = 0f;
    private float moveY = 0f;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float bulletSpeed = 1f;
    public float shootCooldown = 0.1f;
    private bool isShooting = false;
    private float lastShootTime = 0f;
    private Vector2 mouseDirection = Vector2.zero;

    private void Update()
    {
        MovementInput();
        ShootInput();
        RotateInput();
    }
    private void FixedUpdate()
    {
        Move();
        Shoot();
        Rotate();
    }
    private void MovementInput()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
    }
    private void Move()
    {
        Vector2 moveVector = new Vector2(moveX, moveY) * moveSpeed * Time.fixedDeltaTime;
        transform.Translate(moveVector, Space.World);
    }
    private void ShootInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
        }
    }
    private void Shoot()
    {
        if (isShooting && Time.time >= lastShootTime + shootCooldown)
        {
            lastShootTime = Time.time;
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (bulletController != null)
            {
                bulletController.SetBullet(shootPoint.up, bulletSpeed);
            }
        }
    }
    private void RotateInput()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure z is zero for 2D
        mouseDirection = (mousePosition - transform.position).normalized;
    }
    private void Rotate()
    {
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}
