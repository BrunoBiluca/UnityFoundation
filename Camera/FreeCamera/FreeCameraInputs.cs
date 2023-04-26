using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace UnityFoundation.Camera.FreeCamera
{
    public class FreeCameraInputs
    {
        public Vector2 Move => inputActions.Player.Move.ReadValue<Vector2>();
        public Vector2 Look => inputActions.Player.Look.ReadValue<Vector2>();
        public bool IsLocked => Cursor.lockState == CursorLockMode.Locked;

        private readonly FreeCameraInputActions inputActions;

        public FreeCameraInputs(FreeCameraInputActions inputActions)
        {
            this.inputActions = inputActions;
        }

        public void Enable()
        {
            inputActions.Cursor.Lock.performed += OnLock;
            inputActions.Cursor.Release.performed += OnRelease;

            inputActions.Cursor.Enable();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            inputActions.Player.Enable();
        }

        public void Disable()
        {
            inputActions.Player.Disable();
        }

        private void OnLock(CallbackContext ctx)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void OnRelease(CallbackContext ctx)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}