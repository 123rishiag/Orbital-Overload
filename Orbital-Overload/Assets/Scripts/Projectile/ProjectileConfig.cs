using System;
using UnityEngine;

namespace ServiceLocator.Projectile
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "ScriptableObjects/ProjectileConfig")]

    public class ProjectileConfig : ScriptableObject
    {
        public GameObject projectilePrefab;
        public ProjectileData projectileData;
    }

    [Serializable]
    public class ProjectileData
    {
        [Header("Speed Data")]
        public float homingSpeed; // Speed of projectiles while homing

        [Header("Score Data")]
        public int hitScore; // Score increment value
    }
}