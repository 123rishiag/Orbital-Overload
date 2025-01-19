using System;
using UnityEngine;

namespace ServiceLocator.Vision
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "ScriptableObjects/CameraConfig")]

    public class CameraConfig : ScriptableObject
    {
        [Header("Camera Data")]
        public float cameraFollowSpeed; // Speed at which the camera follows the player
        public CameraShakeData[] cameraShakeData; // All Camera Shakes
    }

    [Serializable]
    public class CameraShakeData
    {
        [Header("Camera Shake Data")]
        public CameraShakeType cameraShakeType; // Camera Shake Type
        public float duration; // Shake Duration
        public float magnitude; // Shake Magnitude
    }
}