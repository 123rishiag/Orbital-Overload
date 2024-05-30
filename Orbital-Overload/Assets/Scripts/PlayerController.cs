using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float casualMoveSpeed = 0.0f;
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

    public Transform mainCamera;
    public float cameraFollowSpeed = 2f;
    public float cameraRightMoveSpeed = 1f;

    public bool isHoming = false;
    public float homingSpeed = 500f;
    public bool isShieldActive = false;

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
    private void LateUpdate()
    {
        MoveCamera();
    }
    private void MoveCamera()
    {
        if (mainCamera != null)
        {
            Vector3 playerPosition = transform.position;
            Vector3 cameraPosition = mainCamera.position;

            float verticalExtent = Camera.main.orthographicSize - 1f;
            float horizontalExtent = (verticalExtent * Screen.width / Screen.height) - 1f;

            float cameraLeftEdge = cameraPosition.x - horizontalExtent;
            float cameraRightEdge = cameraPosition.x + horizontalExtent;
            float cameraTopEdge = cameraPosition.y + verticalExtent;
            float cameraBottomEdge = cameraPosition.y - verticalExtent;

            Vector3 newCameraPosition = cameraPosition;

            if (playerPosition.x > cameraRightEdge)
            {
                newCameraPosition.x = playerPosition.x - horizontalExtent;
            }
            else if (playerPosition.x < cameraLeftEdge)
            {
                newCameraPosition.x = playerPosition.x + horizontalExtent;
            }

            if (playerPosition.y > cameraTopEdge)
            {
                newCameraPosition.y = playerPosition.y - verticalExtent;
            }
            else if (playerPosition.y < cameraBottomEdge)
            {
                newCameraPosition.y = playerPosition.y + verticalExtent;
            }

            mainCamera.position = Vector3.Lerp(cameraPosition, newCameraPosition, cameraFollowSpeed * Time.deltaTime);
        }
    }

    private void MovementInput()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        if(moveX == 0f)
        {
            moveX = casualMoveSpeed;
        }
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
                if (isHoming)
                {
                    bulletController.SetHoming(isHoming, homingSpeed);
                }
                bulletController.ShootBullet(shootPoint.up, bulletSpeed);
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

    public void Teleport(float minDistance)
    {
        float maxDistance = minDistance + 3f;

        float angle = Random.Range(0f, Mathf.PI * 2);

        float distance = Random.Range(minDistance, maxDistance);

        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance;
        Vector3 newPosition = transform.position + offset;

        newPosition.x = Mathf.Clamp(newPosition.x, -8f, 8f);
        newPosition.y = Mathf.Clamp(newPosition.y, -4f, 4f);

        transform.position = newPosition;
    }

}
