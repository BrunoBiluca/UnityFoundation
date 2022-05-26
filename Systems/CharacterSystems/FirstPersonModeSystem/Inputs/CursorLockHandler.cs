using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityFoundation.FirstPersonModeSystem
{
    public class CursorLockHandler
    {
        private readonly FirstPersonInputActions inputsActions;

        public CursorLockHandler(FirstPersonInputActions inputsActions)
        {
            this.inputsActions = inputsActions;
        }

        public void Enable()
        {
            inputsActions.Cursor.Lock.performed += OnLock;
            inputsActions.Cursor.Release.performed += OnRelease;

            inputsActions.Cursor.Enable();
            Lock();
        }

        public void Disable()
        {
            inputsActions.Cursor.Disable();
            Release();
        }

        private void OnLock(InputAction.CallbackContext ctx) => Lock();
        private void OnRelease(InputAction.CallbackContext ctx) => Release();

        public void Lock()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Release()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}