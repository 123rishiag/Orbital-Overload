using System;
using UnityEngine;

namespace ServiceLocator.PowerUp
{
    [CreateAssetMenu(fileName = "PowerUpConfig", menuName = "ScriptableObjects/PowerUpConfig")]

    public class PowerUpConfig : ScriptableObject
    {
        public GameObject powerUpPrefab; // Prefab for power-up
        public float powerUpSpawnInterval; // Time interval between spawns
        public float powerUpSpawnRadius; // Radius within which power-ups spawn
        public float powerUpAwayFromPlayerSpawnDistance; // Minimum distance from player to spawn
        public PowerUpData[] powerUpData; // All power-ups

    }

    [Serializable]
    public class PowerUpData
    {
        public PowerUpType powerUpType; // Type of power-up
        public Color powerUpColor; // Color of power-up
        public float powerUpDuration; // Duration of the power-up effect
        public float powerUpValue; // Value of the power-up effect
        public float powerUpLifetime; // Lifetime of the power-up before it expires
    }
}