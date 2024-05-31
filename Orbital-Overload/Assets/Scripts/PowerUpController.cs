using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public PowerUpType powerUpType;
    public float powerUpDuration = 5f;
    public float powerUpValue = 0;
    public float powerUpLifetime = 10f;

    private void Start()
    {
        Destroy(gameObject, powerUpLifetime);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            StartCoroutine(ActivatePowerUp(collider));
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    IEnumerator ActivatePowerUp(Collider2D collider)
    {
        Destroy(gameObject);
        PlayerController playerController = collider.GetComponent<PlayerController>();
        switch (powerUpType)
        {
            case PowerUpType.HomingOrbs:
                playerController.isHoming = true;
                yield return new WaitForSeconds(powerUpDuration);
                playerController.isHoming = false;
                break;
            case PowerUpType.RapidFire:
                playerController.shootCooldown /= powerUpValue;
                yield return new WaitForSeconds(powerUpDuration);
                playerController.shootCooldown *= powerUpValue;
                break;
            case PowerUpType.Shield:
                playerController.isShieldActive = true;
                yield return new WaitForSeconds(powerUpDuration);
                playerController.isShieldActive = false;
                break;
            case PowerUpType.SlowMotion:
                Time.timeScale = powerUpValue;
                yield return new WaitForSeconds(powerUpDuration);
                Time.timeScale = 1f;
                break;
            case PowerUpType.Teleport:
                playerController.Teleport(powerUpValue);
                break;
        }
    }
}

public enum PowerUpType
{
    HomingOrbs,
    RapidFire,
    Shield,
    SlowMotion,
    Teleport
}