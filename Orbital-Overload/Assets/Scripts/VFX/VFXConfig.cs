using System;
using UnityEngine;

namespace ServiceLocator.VFX
{
    [CreateAssetMenu(fileName = "VFXConfig", menuName = "ScriptableObjects/VFXConfig")]

    public class VFXConfig : ScriptableObject
    {
        public VFXView vfxPrefab;
        public VFXData[] vfxData;
    }

    [Serializable]
    public class VFXData
    {
        [Header("VFX Data")]
        public VFXType vfxType; // Type of VFX
        public Sprite vfxSprite; // Sprite of VFX
        public float vfxDuration; // Duration of the vfx
        public float vfxScaleMultiplier; // Scale Multiplier of VFX
    }
}