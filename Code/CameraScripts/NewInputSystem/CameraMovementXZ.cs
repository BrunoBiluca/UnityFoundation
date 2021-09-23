using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.UnityFoundation.Code.CameraScripts.NewInputSystem
{
    public class CameraMovementXZ
    {
        public float CameraSpeed { get; set; } = 20f;
        public float EdgeOffset { get; set; }  = 10f;
        public Vector2 MoveLimitsX { get; set; }
        public Vector2 MoveLimitsZ { get; set; }

        private readonly Transform targetTransform;

        private Vector2 previousAxisInput;

        public CameraMovementXZ(Transform transform)
        {
            targetTransform = transform;
            ActionsBinder();
        }

        public void OnUpdate()
        {
            if(previousAxisInput != Vector2.zero)
                AxisMovement();
            else
                EdgeMovement();
        }

        /// **************************************************
        /// PRIVATE METHODS
        /// **************************************************

        private void ActionsBinder()
        {
            var controls = new CameraMovementInputActions();

            controls.Camera.AxisMovement.performed += SetPreviousInput;
            controls.Camera.AxisMovement.canceled += SetPreviousInput;

            controls.Enable();
        }

        private void SetPreviousInput(InputAction.CallbackContext ctx)
        {
            previousAxisInput = ctx.ReadValue<Vector2>();
        }

        private void AxisMovement()
        {
            UpdatePosition(
                EvaluateMovement(previousAxisInput.x, previousAxisInput.y)
            );
        }
        private void EdgeMovement()
        {
            UpdatePosition(
                EvaluateMovement(EdgeScreenDirectionX(), EdgeScreenDirectionY())
            );
        }

        private Vector3 EvaluateMovement(float xDirection, float zDirection)
        {
            var forward = targetTransform.forward;
            forward.y = 0f;
            var moveDirection = forward * zDirection + targetTransform.right * xDirection;

            return moveDirection.normalized * CameraSpeed * Time.deltaTime;
        }

        private float EdgeScreenDirectionX()
        {
            var mousePosition = Mouse.current.position.ReadValue();

            if(mousePosition.x > Screen.width - EdgeOffset)
                return 1f;
            else if(mousePosition.x < EdgeOffset)
                return -1f;

            return 0f;
        }

        private float EdgeScreenDirectionY()
        {
            var mousePosition = Mouse.current.position.ReadValue();

            if(mousePosition.y > Screen.height - EdgeOffset)
                return 1f;
            else if(mousePosition.y < EdgeOffset)
                return -1f;

            return 0f;
        }

        private void UpdatePosition(Vector3 addPosition)
        {
            var newPos = targetTransform.position + addPosition;
            newPos.x = Mathf.Clamp(newPos.x, MoveLimitsX.x, MoveLimitsX.y);
            newPos.z = Mathf.Clamp(newPos.z, MoveLimitsZ.x, MoveLimitsZ.y);

            targetTransform.position = newPos;
        }

    }
}