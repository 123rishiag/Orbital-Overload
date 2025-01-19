using UnityEngine;

namespace ServiceLocator.VFX
{
    public class VFXController
    {
        // Private Variables
        private VFXModel vfxModel;
        private VFXView vfxView;

        public VFXController(VFXData _vfxData, VFXView _vfxPrefab,
            Transform _vfxParentPanel, Vector3 _spawnPosition)
        {
            // Setting Variables
            vfxModel = new VFXModel(_vfxData);
            vfxView = Object.Instantiate(_vfxPrefab, _spawnPosition, Quaternion.identity, _vfxParentPanel).
                GetComponent<VFXView>();
            vfxView.Init(this);
        }

        // Getters
        public VFXModel GetVFXModel() => vfxModel;
        public VFXView GetVFXView() => vfxView;
    }
}
