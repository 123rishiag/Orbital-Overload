using System;
using UnityEngine;

namespace ServiceLocator.VFX
{
    [CreateAssetMenu(fileName = "VFXConfig", menuName = "ScriptableObjects/VFXConfig")]

    public class VFXConfig : ScriptableObject
    {
        public VFXData[] vfxData;
    }

    [Serializable]
    public class VFXData
    {
        [Header("VFX Data")]
        public VFXType vfxType; // Type of VFX
        public GameObject vfxPrefab;
        public float vfxDuration; // Duration of the vfx
    }
}