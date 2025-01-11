using System;
using UnityEngine;

namespace ServiceLocator.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/PlayerConfig")]
    
    public class PlayerConfig : ScriptableObject
    {
        public PlayerData playerData;
    }

    [Serializable]
    public class PlayerData
    {
        [Header("Prefabs")]
        public GameObject bulletPrefab; // Bullet prefab for shooting

        [Header("Health Elements")]
        public int maxHealth; // Maximum health

        [Header("Movement Elements")]
        public float casualMoveSpeed; // Default move speed when no input
        public float moveSpeed; // Movement speed

        [Header("Bullet Elements")]
        public float bulletSpeed; // Speed of bullets
        public float homingSpeed; // Speed of homing bullets
        public float shootCooldown; // Cooldown between shots

        [Header("Score Elements")]
        public int increaseScoreValue; // Score increment value
    }
}