using ServiceLocator.Event;
using UnityEngine;

namespace ServiceLocator.VFX
{
    public class VFXService
    {
        // Private Variables
        private VFXConfig vfxConfig;
        private Transform vfxParentPanel;
        private VFXPool vfxPool;

        // Private Services
        private EventService eventService;

        public VFXService(VFXConfig _vfxConfig, Transform _vfxParentPanel)
        {
            // Setting Variables
            vfxConfig = _vfxConfig;
            vfxParentPanel = _vfxParentPanel;
        }

        public void Init(EventService _eventService)
        {
            // Setting Services
            eventService = _eventService;

            // Creating Object Pool for vfx
            vfxPool = new VFXPool(vfxConfig, vfxParentPanel);

            // Adding Listeners
            eventService.OnCreateVFXEvent.AddListener(CreateVFX);
        }

        public void Destroy()
        {
            // Removing Listeners
            eventService.OnCreateVFXEvent.RemoveListener(CreateVFX);
        }

        private void CreateVFX(VFXType _vfxType, Transform _vfxTransform, Color _vfxColor)
        {
            // Fetching VFX
            switch (_vfxType)
            {
                case VFXType.Splatter:
                    vfxPool.GetVFX<VFXController>(_vfxTransform, _vfxColor, _vfxType);
                    break;
                default:
                    Debug.LogWarning($"Unhandled VFXType: {_vfxType}");
                    break;
            }
        }

        public void Update()
        {
            ProcessVFXUpdate();
        }

        private void ProcessVFXUpdate()
        {
            for (int i = vfxPool.pooledItems.Count - 1; i >= 0; i--)
            {
                // Skipping if the pooled item's isUsed is false
                if (!vfxPool.pooledItems[i].isUsed)
                {
                    continue;
                }

                var vfxController = vfxPool.pooledItems[i].Item;

                if (!vfxController.IsActive())
                {
                    ReturnVFXToPool(vfxController);
                }
            }
        }

        public void Reset()
        {
            // Disabling All VFX
            for (int i = vfxPool.pooledItems.Count - 1; i >= 0; i--)
            {
                var powerUpController = vfxPool.pooledItems[i].Item;
                ReturnVFXToPool(powerUpController);
            }
        }

        private void ReturnVFXToPool(VFXController _vfxToReturn)
        {
            _vfxToReturn.GetVFXView().HideView();
            vfxPool.ReturnItem(_vfxToReturn);
        }
    }
}