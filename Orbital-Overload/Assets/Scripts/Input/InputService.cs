
using UnityEngine;

namespace ServiceLocator.Control
{
    public class InputService
    {
        // Private Variables
        private InputControls inputControls;

        public InputService()
        {
            // Setting Variables
            inputControls = new InputControls();

            // Setting Elements
            inputControls.Enable();
        }

        public void Update()
        {
            PlayerInput();
        }

        private void PlayerInput()
        {
            GetPlayerMovement = inputControls.Player.Move.ReadValue<Vector2>();
            IsPlayerShooting = inputControls.Player.Shoot.IsPressed();
            GetMousePosition = inputControls.General.MousePosition.ReadValue<Vector2>();
            IsEscapePressed = inputControls.General.Escape.WasPerformedThisFrame();
        }

        // Getters
        public Vector2 GetPlayerMovement { get; private set; }
        public bool IsPlayerShooting { get; private set; }
        public Vector2 GetMousePosition { get; private set; }
        public bool IsEscapePressed { get; private set; }
    }
}
