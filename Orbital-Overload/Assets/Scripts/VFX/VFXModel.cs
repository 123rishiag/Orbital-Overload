using UnityEngine;

namespace ServiceLocator.VFX
{
    public class VFXModel
    {
        public VFXModel(VFXData _vfxData)
        {
            Reset(_vfxData);
        }

        public void Reset(VFXData _vfxData)
        {
            VFXType = _vfxData.vfxType;
            VFXSprite = _vfxData.vfxSprite;
            VFXDuration = _vfxData.vfxDuration;
        }

        // Getters & Setters
        public VFXType VFXType { get; private set; }
        public Sprite VFXSprite { get; private set; }
        public float VFXDuration { get; private set; }
    }
}