using Cinemachine;
using ServiceLocator.Event;
using System;
using UnityEngine;

namespace ServiceLocator.Vision
{
    public class CameraService
    {
        // Private Variables
        private CameraConfig cameraConfig;
        private CinemachineVirtualCamera virtualCamera;
        private Transform cameraDefaultTransform;
        private CinemachineImpulseSource impulseSource;

        // Private Services
        private EventService eventService;

        public CameraService(CameraConfig _cameraConfig, CinemachineVirtualCamera _virtualCamera)
        {
            // Setting Variables
            cameraConfig = _cameraConfig;
            virtualCamera = _virtualCamera;
            cameraDefaultTransform = _virtualCamera.transform;
            impulseSource = virtualCamera.gameObject.GetComponent<CinemachineImpulseSource>();
        }

        public void Init(EventService _eventService)
        {
            // Setting Services
            eventService = _eventService;

            // Adding Listeners
            eventService.OnDoShakeScreenEvent.AddListener(DoShakeScreen);
        }

        public void Destroy()
        {
            // Removing Listeners
            eventService.OnDoShakeScreenEvent.RemoveListener(DoShakeScreen);
        }

        public void Reset()
        {
            virtualCamera.transform.position = cameraDefaultTransform.position;
        }

        public void DoShakeScreen(CameraShakeType _cameraShakeType)
        {
            // Fetching Values
            int cameraShakeIndex =
                Array.FindIndex(cameraConfig.cameraShakeData, data => data.cameraShakeType == _cameraShakeType);
            CameraShakeData cameraShakeData = cameraConfig.cameraShakeData[cameraShakeIndex];

            // Trigger the shake
            impulseSource.GenerateImpulse(Vector3.one * cameraShakeData.magnitude);
        }

        public void SetFollowTarget(Transform _target)
        {
            virtualCamera.Follow = _target;
        }
    }
}