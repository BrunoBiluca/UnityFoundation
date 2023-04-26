using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace UnityFoundation.FirstPersonModeSystem
{
    public class FirstPersonInputs : IFirstPersonInputs
    {
        public Vector2 Move { get; private set; }
        public bool Jump { get; private set; }
        public Vector2 Look => inputActions.Player.Look.ReadValue<Vector2>();
        public bool Aim => inputActions.Player.Aim.triggered;
        public bool Fire => inputActions.Player.Fire.triggered;
        public bool Reload => inputActions.Player.Reload.triggered;

        private readonly FirstPersonInputActions inputActions;

        public FirstPersonInputs(FirstPersonInputActions inputActions)
        {
            this.inputActions = inputActions;
        }

        public void Enable()
        {
            inputActions.Player.Move.performed += OnMove;
            inputActions.Player.Move.started += OnMove;
            inputActions.Player.Move.canceled += OnMove;

            inputActions.Player.Jump.performed += OnJump;
            inputActions.Player.Jump.started += OnJump;
            inputActions.Player.Jump.canceled += OnJump;

            inputActions.Player.Enable();
        }

        public void Disable()
        {
            inputActions.Player.Disable();
        }

        private void OnMove(CallbackContext ctx) => Move = ctx.ReadValue<Vector2>();

        private void OnJump(CallbackContext ctx) => Jump = ctx.ReadValueAsButton();
    }
}