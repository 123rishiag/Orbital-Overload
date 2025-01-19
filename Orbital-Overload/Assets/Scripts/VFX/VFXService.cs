using ServiceLocator.Event;
using System;
using UnityEngine;

namespace ServiceLocator.VFX
{
    public class VFXService
    {
        // Private Variables
        private VFXConfig vfxConfig;
        private Transform vfxParentPanel;

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

            // Adding Listeners
            eventService.OnCreateVFXEvent.AddListener(CreateVFX);
        }

        public void Destroy()
        {
            // Removing Listeners
            eventService.OnCreateVFXEvent.RemoveListener(CreateVFX);
        }

        private void CreateVFX(VFXType _vfxType, Vector3 _spawnPosition)
        {
            int vfxIndex = Array.FindIndex(vfxConfig.vfxData, data => data.vfxType == _vfxType);
            VFXData vfxData = vfxConfig.vfxData[vfxIndex];
            VFXController vfxController = new VFXController(vfxData, vfxConfig.vfxPrefab, vfxParentPanel, _spawnPosition);
        }
    }
}