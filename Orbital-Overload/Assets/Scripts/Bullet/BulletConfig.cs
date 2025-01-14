using System;
using UnityEngine;

namespace ServiceLocator.Bullet
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "ScriptableObjects/BulletConfig")]

    public class BulletConfig : ScriptableObject
    {
        public GameObject bulletPrefab;
        public BulletData bulletData;
    }

    [Serializable]
    public class BulletData
    {
        [Header("Speed Data")]
        public float homingSpeed; // Speed of bullets while homing

        [Header("Score Data")]
        public int hitScore; // Score increment value
    }
}