using System;
using UnityEngine;

namespace ServiceLocator.Bullet
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "ScriptableObjects/BulletConfig")]

    public class BulletConfig : ScriptableObject
    {
        public BulletData bulletData;
    }

    [Serializable]
    public class BulletData
    {
        [Header("Prefabs")]
        public GameObject bulletPrefab; // Bullet prefab for shooting

        [Header("Speed Data")]
        public float homingSpeed; // Speed of bullets while homing
    }
}