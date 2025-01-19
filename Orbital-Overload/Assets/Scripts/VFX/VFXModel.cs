using UnityEngine;

namespace ServiceLocator.VFX
{
    public class VFXModel
    {
        public VFXModel(VFXData _vfxData, Transform _vfxTransform, Color _vfxColor)
        {
            Reset(_vfxData, _vfxTransform, _vfxColor);
        }

        public void Reset(VFXData _vfxData, Transform _vfxTransform, Color _vfxColor)
        {
            VFXType = _vfxData.vfxType;
            VFXSprite = _vfxData.vfxSprite;
            VFXDuration = _vfxData.vfxDuration;
            VFXScaleMultiplier = _vfxData.vfxScaleMultiplier;
            VFXTransform = _vfxTransform;
            VFXColor = _vfxColor;
        }

        // Getters & Setters
        public VFXType VFXType { get; private set; }
        public Sprite VFXSprite { get; private set; }
        public float VFXDuration { get; private set; }
        public float VFXScaleMultiplier { get; private set; }
        public Transform VFXTransform { get; private set; }
        public Color VFXColor { get; private set; }
    }
}