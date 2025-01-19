using UnityEngine;

namespace ServiceLocator.VFX
{
    public class VFXController
    {
        // Private Variables
        private VFXModel vfxModel;
        private VFXView vfxView;

        public VFXController(VFXData _vfxData, VFXView _vfxPrefab,
            Transform _vfxParentPanel, Transform _vfxTransform, Color _vfxColor)
        {
            // Setting Variables
            vfxModel = new VFXModel(_vfxData, _vfxTransform, _vfxColor);
            vfxView = Object.Instantiate(_vfxPrefab, _vfxTransform.position, Quaternion.identity, _vfxParentPanel).
                GetComponent<VFXView>();
            vfxView.Init(this);
        }

        // Getters
        public VFXModel GetVFXModel() => vfxModel;
        public VFXView GetVFXView() => vfxView;
    }
}
