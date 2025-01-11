using System;
using UnityEngine;

namespace ServiceLocator.Enemy
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/EnemyConfig")]

    public class EnemyConfig : ScriptableObject
    {
        public GameObject enemyPrefab; // Enemy prefab to spawn
        public float enemySpawnInterval; // Time interval between spawns
        public float enemySpawnRadius; // Radius within which enemies spawn
        public float enemyAwayFromPlayerSpawnDistance; // Minimum distance from player for enemy spawn
        public EnemyData enemyData;
    }

    [Serializable]
    public class EnemyData
    {
        [Header("Movement Data")]
        public float moveSpeed; // Movement speed

        [Header("Shooting Data")]
        public float shootSpeed; // Speed of shooting
        public float shootCooldown; // Cooldown between shots

        [Header("Distance Data")]
        public float enemyAwayFromPlayerMinDistance; // Minimum distance from player enemy should maintain
    }
}