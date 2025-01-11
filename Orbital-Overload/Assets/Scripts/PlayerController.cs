using ServiceLocator.Main;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float casualMoveSpeed = 0.0f; // Default move speed when no input
    [SerializeField] private float moveSpeed = 1f; // Movement speed
    [SerializeField] private GameObject bulletPrefab; // Bullet prefab for shooting
    [SerializeField] private Transform shootPoint; // Point from where bullets are shot
    [SerializeField] private float bulletSpeed = 1f; // Speed of bullets
    [SerializeField] private float homingSpeed = 500f; // Speed of homing bullets
    [SerializeField] private float shootCooldown = 0.1f; // Cooldown between shots
    [SerializeField] private Transform mainCamera; // Main camera reference
    [SerializeField] private float cameraFollowSpeed = 2f; // Speed at which the camera follows the player
    [SerializeField] private TextMeshProUGUI powerUpText; // Text for displaying power-up status
    [SerializeField] private int increaseScoreValue = 10; // Score increment value
    [SerializeField] private TextMeshProUGUI scoreText; // Text for displaying score
    [SerializeField] private GameObject gameManager; // Game manager reference
    [SerializeField] private int maxHealth = 3; // Maximum health
    [SerializeField] private TextMeshProUGUI healthText; // Text for displaying health

    private float moveX = 0f; // X-axis movement input
    private float moveY = 0f; // Y-axis movement input
    private bool isShooting = false; // Shooting state
    private float lastShootTime = 0f; // Time of last shot
    private Vector2 mouseDirection = Vector2.zero; // Direction of the mouse
    private bool isShieldActive = false; // Whether shield is active
    private bool isHoming = false; // Whether bullets are homing
    
    private int score = 0; // Player score
    private int health = 0; // Player health
    private GameService gameController; // Game manager controller

    private void Start()
    {
        health = maxHealth; // Initialize health
        UpdateHealthText(); // Update health text
    }

    private void Update()
    {
        MovementInput(); // Handle movement input
        ShootInput(); // Handle shoot input
        RotateInput(); // Handle rotate input
    }

    private void FixedUpdate()
    {
        Move(); // Handle movement
        Shoot(); // Handle shooting
        Rotate(); // Handle rotation
    }

    private void LateUpdate()
    {
        MoveCamera(); // Handle camera movement
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

            // Move camera to follow player
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
        moveX = Input.GetAxis("Horizontal"); // Get horizontal input
        moveY = Input.GetAxis("Vertical"); // Get vertical input
        if (moveX == 0f)
        {
            moveX = casualMoveSpeed; // Default move speed
        }
    }

    private void Move()
    {
        Vector2 moveVector = new Vector2(moveX, moveY) * moveSpeed * Time.fixedDeltaTime;
        transform.Translate(moveVector, Space.World); // Move player
    }

    private void ShootInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true; // Start shooting
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isShooting = false; // Stop shooting
        }
    }

    private void Shoot()
    {
        if (isShooting && Time.time >= lastShootTime + shootCooldown)
        {
            lastShootTime = Time.time; // Update last shoot time
            GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (bulletController != null)
            {
                bulletController.SetOwnerTag(gameObject.tag); // Set owner tag to avoid self-collision
                if (isHoming)
                {
                    bulletController.SetHoming(isHoming, homingSpeed); // Set homing properties
                }
                bulletController.ShootBullet(shootPoint.up, bulletSpeed); // Shoot the bullet
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
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Rotate player to face mouse
    }

    public void AddScore()
    {
        score += increaseScoreValue; // Increase score
        UpdateScoreText(); // Update score text
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; // Display score
    }

    public void UpdatePowerUpText(string text)
    {
        powerUpText.text = text; // Display power-up text
    }

    private void UpdateHealthText()
    {
        healthText.text = "Health: " + health; // Display health
    }

    public void ActivatePowerUp(PowerUpType powerUpType, float powerUpDuration, float powerUpValue)
    {
        StartCoroutine(PowerUp(powerUpType, powerUpDuration, powerUpValue)); // Activate power-up effect
    }

    private IEnumerator PowerUp(PowerUpType powerUpType, float powerUpDuration, float powerUpValue)
    {
        string powerUpText;
        if (powerUpType == PowerUpType.HealthPick || powerUpType == PowerUpType.Teleport)
        {
            powerUpText = powerUpType.ToString() + "ed.";
        }
        else
        {
            powerUpText = powerUpType.ToString() + " activated for " + powerUpDuration.ToString() + " seconds.";
        }
        UpdatePowerUpText(powerUpText);
        switch (powerUpType)
        {
            case PowerUpType.HealthPick:
                IncreaseHealth((int)powerUpValue); // Increase health
                yield return new WaitForSeconds(powerUpDuration);
                break;
            case PowerUpType.HomingOrbs:
                isHoming = true; // Activate homing bullets
                yield return new WaitForSeconds(powerUpDuration);
                isHoming = false; // Deactivate homing bullets
                break;
            case PowerUpType.RapidFire:
                shootCooldown /= powerUpValue; // Increase fire rate
                yield return new WaitForSeconds(powerUpDuration);
                shootCooldown *= powerUpValue; // Reset fire rate
                break;
            case PowerUpType.Shield:
                isShieldActive = true; // Activate shield
                yield return new WaitForSeconds(powerUpDuration);
                isShieldActive = false; // Deactivate shield
                break;
            case PowerUpType.SlowMotion:
                Time.timeScale = powerUpValue; // Slow down time
                yield return new WaitForSeconds(powerUpDuration);
                Time.timeScale = 1f; // Reset time
                break;
            case PowerUpType.Teleport:
                Teleport(powerUpValue); // Teleport player
                yield return new WaitForSeconds(powerUpDuration);
                break;
            default:
                yield return new WaitForSeconds(1);
                break;
        }
        UpdatePowerUpText(null); // Clear power-up text
    }

    public void Teleport(float minDistance)
    {
        float maxDistance = minDistance + 3f;

        float angle = Random.Range(0f, Mathf.PI * 2);

        float distance = Random.Range(minDistance, maxDistance);

        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance;
        Vector3 newPosition = transform.position + offset;

        newPosition.x = Mathf.Clamp(newPosition.x, -8f, 8f); // Clamp x position
        newPosition.y = Mathf.Clamp(newPosition.y, -4f, 4f); // Clamp y position

        transform.position = newPosition; // Teleport player
    }

    public void DecreaseHealth()
    {
        health -= 1; // Decrease health
        if (health < 0)
        {
            health = 0;
        }
        if (health == 0)
        {
            PlayerDie(); // Handle player death
        }
        UpdateHealthText();
    }

    private void IncreaseHealth(int increaseHealth)
    {
        health += increaseHealth; // Increase health
        if (health > maxHealth)
        {
            health = maxHealth; // Clamp health to max
        }
        else
        {
            SoundManager.Instance.PlayEffect(SoundType.PlayerHeal); // Play heal sound effect
        }
        UpdateHealthText();
    }

    public bool ShieldActive()
    {
        return isShieldActive;
    }
    private void PlayerDie()
    {
        //gameController.GameOver(); // Trigger game over
    }
}