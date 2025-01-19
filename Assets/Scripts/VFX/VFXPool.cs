using ServiceLocator.Utility;
using System;
using UnityEngine;

namespace ServiceLocator.VFX
{
    public class VFXPool : GenericObjectPool<VFXController>
    {
        // Private Variables
        private VFXConfig vfxConfig;
        private Transform vfxParentPanel;

        private Transform vfxTransform;
        private Color vfxColor;
        private VFXType vfxType;

        public VFXPool(VFXConfig _vfxConfig, Transform _vfxParentPanel)
        {
            // Setting Variables
            vfxConfig = _vfxConfig;
            vfxParentPanel = _vfxParentPanel;
        }

        public VFXController GetVFX<T>(Transform _vfxTransform, Color _vfxColor, VFXType _vfxType) where T : VFXController
        {
            // Setting Variables
            vfxTransform = _vfxTransform;
            vfxColor = _vfxColor;
            vfxType = _vfxType;

            // Fetching Item
            var item = GetItem<T>();

            // Fetching Index
            int vfxIndex = GetVFXIndex();

            // Resetting Item Properties
            item.Reset(vfxConfig.vfxData[vfxIndex], vfxTransform, vfxColor);

            return item;
        }

        protected override VFXController CreateItem<T>()
        {
            // Fetching Index
            int vfxIndex = GetVFXIndex();

            // Creating Controller
            switch (vfxType)
            {
                case VFXType.Splatter:
                    return new VFXController(vfxConfig.vfxData[vfxIndex], vfxConfig.vfxPrefab,
                        vfxParentPanel, vfxTransform, vfxColor);
                default:
                    Debug.LogWarning($"Unhandled VFXType: {vfxType}");
                    return null;
            }
        }

        // Getters
        private int GetVFXIndex()
        {
            // Fetching Index
            return Array.FindIndex(vfxConfig.vfxData, data => data.vfxType == vfxType);
        }
    }
}