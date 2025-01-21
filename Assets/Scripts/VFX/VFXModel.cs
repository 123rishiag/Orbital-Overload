using UnityEngine;

namespace ServiceLocator.VFX
{
    public class VFXModel
    {
        public VFXModel(VFXData _vfxData, Color _vfxColor)
        {
            Reset(_vfxData, _vfxColor);
        }

        public void Reset(VFXData _vfxData, Color _vfxColor)
        {
            VFXType = _vfxData.vfxType;
            VFXColor = _vfxColor;
            VFXSprite = _vfxData.vfxSprite;
            VFXDuration = _vfxData.vfxDuration;
            VFXScaleMultiplier = _vfxData.vfxScaleMultiplier;
        }

        // Getters & Setters
        public VFXType VFXType { get; private set; }
        public Color VFXColor { get; private set; }
        public Sprite VFXSprite { get; private set; }
        public float VFXDuration { get; private set; }
        public float VFXScaleMultiplier { get; private set; }
    }
}