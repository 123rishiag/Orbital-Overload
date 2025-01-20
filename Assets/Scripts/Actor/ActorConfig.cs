using System;
using UnityEngine;

namespace ServiceLocator.Actor
{
    [CreateAssetMenu(fileName = "ActorConfig", menuName = "ScriptableObjects/ActorConfig")]

    public class ActorConfig : ScriptableObject
    {
        [Header("Prefab")]
        public ActorView actorPrefab; // Actor prefab to spawn

        [Header("Player Data")]
        public float playerCasualMoveSpeed; // Default move speed when no input
        public ActorData playerData; // Data for Actor

        [Header("Enemy Data")]
        public float enemySpawnInterval; // Time interval between spawns
        public float enemySpawnRadius; // Radius within which enemies spawn
        public float enemyAwayFromPlayerSpawnDistance; // Minimum distance from player for enemy spawn
        public float enemyAwayFromPlayerMinDistance; // Minimum distance from player enemy should maintain
        public ActorData[] enemyData; // All the Enemies
    }

    [Serializable]
    public class ActorData
    {
        [Header("Actor Data")]
        public ActorType actorType; // Actor Type
        public Color actorColor; // Color of Actor
        public Sprite actorSprite; // Sprite of Actor
        public Sprite actorShooterSprite; // Actor's Shooter Sprite

        [Header("Health Data")]
        public int maxHealth; // Maximum health

        [Header("Movement Data")]
        public float moveSpeed; // Movement speed

        [Header("Shooting Data")]
        public float shootSpeed; // Speed of shooting
        public float shootCooldown; // Cooldown between shots
    }
}