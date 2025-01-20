using System;
using UnityEngine;

namespace ServiceLocator.Projectile
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "ScriptableObjects/ProjectileConfig")]

    public class ProjectileConfig : ScriptableObject
    {
        public ProjectileView projectilePrefab;
        public ProjectileData[] projectileData;
    }

    [Serializable]
    public class ProjectileData
    {
        [Header("Projectile Data")]
        public ProjectileType projectileType; // Type of Projectile

        [Header("Score Data")]
        public int hitScore; // Score increment value
    }
}