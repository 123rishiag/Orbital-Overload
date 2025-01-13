using ServiceLocator.Player;
using UnityEngine;

namespace ServiceLocator.Vision
{
    public class CameraService
    {
        // Private Variables
        private Camera mainCamera; // Main camera reference
        private float cameraFollowSpeed; // Speed at which the camera follows the player

        // Private Services
        private PlayerService playerService;

        public CameraService(Camera _camera, float _cameraFollowSpeed, PlayerService _playerService)
        {
            // Setting Variables
            mainCamera = _camera;
            cameraFollowSpeed = _cameraFollowSpeed;

            // Setting Services
            playerService = _playerService;
        }

        public void LateUpdate()
        {
            FollowCameraTowardsPlayer(); // Handle camera movement
        }

        private void FollowCameraTowardsPlayer()
        {
            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 playerPosition = playerService.GetPlayerController().GetPlayerView().GetPosition();

            float verticalExtent = mainCamera.orthographicSize - 1f;
            float horizontalExtent = (verticalExtent * Screen.width / Screen.height) - 1f;

            float cameraLeftEdge = cameraPosition.x - horizontalExtent;
            float cameraRightEdge = cameraPosition.x + horizontalExtent;
            float cameraTopEdge = cameraPosition.y + verticalExtent;
            float cameraBottomEdge = cameraPosition.y - verticalExtent;

            Vector3 newCameraPosition = cameraPosition;

            // Move camera to follow player
            if (playerPosition.x > cameraRightEdge)
            {
                newCameraPosition.x = playerPosition.x - horizontalExtent;
            }
            else if (playerPosition.x < cameraLeftEdge)
            {
                newCameraPosition.x = playerPosition.x + horizontalExtent;
            }

            if (playerPosition.y > cameraTopEdge)
            {
                newCameraPosition.y = playerPosition.y - verticalExtent;
            }
            else if (playerPosition.y < cameraBottomEdge)
            {
                newCameraPosition.y = playerPosition.y + verticalExtent;
            }

            mainCamera.transform.position = Vector3.Lerp(cameraPosition, newCameraPosition, cameraFollowSpeed * Time.deltaTime);
        }
    }
}