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
            vfxModel = new VFXModel(_vfxData, _vfxColor);
            vfxView = Object.Instantiate(_vfxPrefab, _vfxTransform.position, Quaternion.identity, _vfxParentPanel).
                GetComponent<VFXView>();
            vfxView.Init(this);
            vfxView.SetTransform(_vfxTransform);
        }

        public void Reset(VFXData _vfxData, Transform _vfxTransform, Color _vfxColor)
        {
            vfxModel.Reset(_vfxData, _vfxColor);
            vfxView.Reset();
            vfxView.SetTransform(_vfxTransform);
            vfxView.ShowView();
        }

        public bool IsActive()
        {
            if (!vfxView.gameObject.activeInHierarchy) return false;
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(vfxView.transform.position);
            if (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1)
            {
                return false;
            }
            return true;
        }

        // Getters
        public VFXModel GetVFXModel() => vfxModel;
        public VFXView GetVFXView() => vfxView;
    }
}
