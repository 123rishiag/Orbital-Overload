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
            PlayerController playerController = collider.GetComponent<PlayerController>();
            playerController.ActivatePowerUp(powerUpType, powerUpDuration, powerUpValue);
            Destroy(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}

public enum PowerUpType
{
    HealthPick,
    HomingOrbs,
    RapidFire,
    Shield,
    SlowMotion,
    Teleport
}