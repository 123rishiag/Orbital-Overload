using UnityEngine;

namespace ServiceLocator.Vision
{
    public class CameraService
    {
        // Private Variables
        private Camera mainCamera; // Main camera reference
        private float cameraFollowSpeed; // Speed at which the camera follows the player
        private Transform cameraDefaultTransform;

        public CameraService(Camera _camera, float _cameraFollowSpeed)
        {
            // Setting Variables
            mainCamera = _camera;
            cameraFollowSpeed = _cameraFollowSpeed;
            cameraDefaultTransform = _camera.transform;
        }

        public void Reset()
        {
            mainCamera.transform.position = cameraDefaultTransform.position;
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

            mainCamera.transform.position = Vector3.Lerp(cameraPosition, newCameraPosition, cameraFollowSpeed * Time.deltaTime);
        }
    }
}