using System;
using UnityEngine;

namespace ServiceLocator.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/PlayerConfig")]

    public class PlayerConfig : ScriptableObject
    {
        public GameObject playerPrefab; // Player prefab to spawn
        public PlayerData playerData;
    }

    [Serializable]
    public class PlayerData
    {
        [Header("Health Data")]
        public int maxHealth; // Maximum health

        [Header("Movement Data")]
        public float casualMoveSpeed; // Default move speed when no input
        public float moveSpeed; // Movement speed

        [Header("Shooting Data")]
        public float shootSpeed; // Speed of shooting
        public float shootCooldown; // Cooldown between shots

        [Header("Score Data")]
        public int increaseScoreValue; // Score increment value
    }
}