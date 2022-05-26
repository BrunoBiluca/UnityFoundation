using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityFoundation.Code;

namespace UnityFoundation.CameraMovementXZ
{
    public class CameraMovementXZ
    {
        public float CameraSpeed { get; set; } = 20f;
        public bool EnabledEdgeMovement { get; set; }
        public float EdgeOffset { get; set; } = 10f;
        public Vector2 MoveLimitsX { get; set; }
        public Vector2 MoveLimitsY { get; set; }
        public Vector2 MoveLimitsZ { get; set; }

        public bool EnableZoomMovement { get; set; }
        public float ZoomAmount { get; set; } = 2f;
        public float ZoomSpeed { get; set; } = 5f;

        private Transform target;

        private Vector2 previousAxisInput;

        private float previousZoomInput;
        private float targetZoom;

        public CameraMovementXZ(Transform transform)
        {
            SetTargetTransform(transform);
            ActionsBinder();
        }

        public void SetTargetTransform(Transform target)
        {
            this.target = target;
            targetZoom = target.transform.position.y;
        }

        public void OnUpdate()
        {
            if(target == null) return;

            if(previousAxisInput != Vector2.zero)
                AxisMovement();
            else
                EdgeMovement();

            ZoomMovement();
        }

        /// **************************************************
        /// PRIVATE METHODS
        /// **************************************************

        private void ActionsBinder()
        {
            var controls = new CameraMovementInputActions();

            controls.Camera.AxisMovement.performed += SetPreviousInput;
            controls.Camera.AxisMovement.canceled += SetPreviousInput;

            controls.Camera.Zoom.performed += SetZoomInput;
            controls.Camera.Zoom.canceled += SetZoomInput;

            controls.Enable();
        }

        private void SetZoomInput(InputAction.CallbackContext ctx)
        {
            previousZoomInput = ctx.ReadValue<float>();
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
            if(!EnabledEdgeMovement) return;

            UpdatePosition(
                EvaluateMovement(EdgeScreenDirectionX(), EdgeScreenDirectionY())
            );
        }

        private void ZoomMovement()
        {
            if(!EnableZoomMovement) return;

            var zoomInput = (-previousZoomInput).Normalize();

            targetZoom += zoomInput * ZoomAmount;
            targetZoom = Mathf.Clamp(
                targetZoom,
                MoveLimitsY.x != 0f ? MoveLimitsY.x : float.MinValue,
                MoveLimitsY.y != 0f ? MoveLimitsY.y : float.MaxValue
            );

            var positionY = Mathf.Lerp(
                target.position.y,
                targetZoom,
                Time.deltaTime * ZoomSpeed
            );

            target.position = new Vector3(
                target.position.x, positionY, target.position.z
            );
        }

        private Vector3 EvaluateMovement(float xDirection, float zDirection)
        {
            var moveDirection = Vector3.forward * zDirection
                + Vector3.right * xDirection;

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
            var newPos = target.position + addPosition;
            newPos.x = Mathf.Clamp(newPos.x, MoveLimitsX.x, MoveLimitsX.y);
            newPos.y = Mathf.Clamp(
                newPos.y,
                MoveLimitsY.x != 0f ? MoveLimitsY.x : float.MinValue,
                MoveLimitsY.y != 0f ? MoveLimitsY.y : float.MaxValue
            );
            newPos.z = Mathf.Clamp(newPos.z, MoveLimitsZ.x, MoveLimitsZ.y);

            target.position = newPos;
        }

    }
}