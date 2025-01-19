using ServiceLocator.Event;
using ServiceLocator.Main;
using System;
using System.Collections;
using UnityEngine;

namespace ServiceLocator.Vision
{
    public class CameraService
    {
        // Private Variables
        private CameraConfig cameraConfig;
        private Camera mainCamera; // Main camera reference
        private Transform cameraDefaultTransform;

        // Private Services
        private EventService eventService;

        public CameraService(CameraConfig _cameraConfig, Camera _camera)
        {
            // Setting Variables
            cameraConfig = _cameraConfig;
            mainCamera = _camera;
            cameraDefaultTransform = _camera.transform;
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
            mainCamera.transform.position = cameraDefaultTransform.position;
        }

        private IEnumerator ShakeScreen(float _duration, float _magnitude)
        {
            Vector3 originalPosition = mainCamera.transform.localPosition;
            float elapsed = 0.0f;

            // Variables for controlling shake behavior
            float noiseFrequency = 10.0f;  // Controls the speed of Perlin noise oscillation

            while (elapsed < _duration)
            {
                // Calculate decay factor to reduce magnitude over time
                float decayFactor = Mathf.Lerp(_magnitude, 0, elapsed / _duration);

                // Generate Perlin noise-based offsets
                float x = (Mathf.PerlinNoise(Time.time * noiseFrequency, 0) * 2 - 1) * decayFactor;
                float y = (Mathf.PerlinNoise(0, Time.time * noiseFrequency) * 2 - 1) * decayFactor;

                // Apply offset to original position
                mainCamera.transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

                elapsed += Time.deltaTime;
                yield return null;
            }

            mainCamera.transform.localPosition = originalPosition;
        }

        public void DoShakeScreen(CameraShakeType _cameraShakeType)
        {
            // Fetching Values
            int cameraShakeIndex =
                Array.FindIndex(cameraConfig.cameraShakeData, data => data.cameraShakeType == _cameraShakeType);
            CameraShakeData cameraShakeData = cameraConfig.cameraShakeData[cameraShakeIndex];

            eventService.OnGetGameControllerEvent.Invoke<GameController>().
                StartManagedCoroutine(ShakeScreen(cameraShakeData.duration, cameraShakeData.magnitude));
        }

        public void FollowCameraTowardsPosition(Vector3 _position)
        {
            Vector3 cameraPosition = mainCamera.transform.position;

            float verticalExtent = mainCamera.orthographicSize - 1f;
            float horizontalExtent = (verticalExtent * Screen.width / Screen.height) - 1f;

            float cameraLeftEdge = cameraPosition.x - horizontalExtent;
            float cameraRightEdge = cameraPosition.x + horizontalExtent;
            float cameraTopEdge = cameraPosition.y + verticalExtent;
            float cameraBottomEdge = cameraPosition.y - verticalExtent;

            Vector3 newCameraPosition = cameraPosition;

            // Move camera to follow player
            if (_position.x > cameraRightEdge)
            {
                newCameraPosition.x = _position.x - horizontalExtent;
            }
            else if (_position.x < cameraLeftEdge)
            {
                newCameraPosition.x = _position.x + horizontalExtent;
            }

            if (_position.y > cameraTopEdge)
            {
                newCameraPosition.y = _position.y - verticalExtent;
            }
            else if (_position.y < cameraBottomEdge)
            {
                newCameraPosition.y = _position.y + verticalExtent;
            }

            mainCamera.transform.position = Vector3.Lerp(cameraPosition, newCameraPosition,
                cameraConfig.cameraFollowSpeed * Time.deltaTime);
        }
    }
}