using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityFoundation.Code;

namespace UnityFoundation.CameraMovementXZ
{
    public class CameraMovementXZ
    {
        [Serializable]
        public class Settings
        {
            public float CameraSpeed { get; set; } = 20f;
            public bool EnabledEdgeMovement { get; set; }
            public float EdgeOffset { get; set; } = 10f;
            public Rangef MoveLimitsX { get; set; }
            public Rangef MoveLimitsY { get; set; }
            public Rangef MoveLimitsZ { get; set; }

            public bool EnableZoomMovement { get; set; }
            public float ZoomAmount { get; set; } = 2f;
            public float ZoomSpeed { get; set; } = 5f;
            public bool EnabledYRotation { get; set; } = true;
            public float YRotationSpeed { get; set; } = 10f;

            public bool EnabledXRotation { get; set; } = true;
            public Rangef XRotationAngle { get; set; }
        }

        public Settings Conf { get; private set; }

        private Transform target;

        private Vector2 previousAxisInput;
        private float previousZoomInput;
        private float previousYRotation;

        private float targetZoom;
        private float targetXAngle;

        public CameraMovementXZ(Transform transform)
        {
            Conf = new Settings();
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

            UpdateXRotation();

            UpdateYRotation();
        }

        private void UpdateXRotation()
        {
            if(!Conf.EnabledXRotation) return;

            targetXAngle = MathX.Remap(
                target.position.y,
                Conf.MoveLimitsY.Start, Conf.MoveLimitsY.End,
                Conf.XRotationAngle.Start, Conf.XRotationAngle.End
            );
            target.rotation = Quaternion.Euler(
                targetXAngle, 
                target.rotation.eulerAngles.y, 
                target.rotation.eulerAngles.z
            );
        }

        private void UpdateYRotation()
        {
            if(!Conf.EnabledYRotation) return;

            var rotationYSpeed = Conf.YRotationSpeed;
            var rotation = previousYRotation * rotationYSpeed * Time.deltaTime;
            target.Rotate(new Vector3(0f, rotation, 0f), Space.World);
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

            if(Conf.EnabledYRotation)
            {
                controls.Camera.YRotate.performed += SetYRotate;
                controls.Camera.YRotate.canceled += SetYRotate;
            }
            controls.Enable();
        }

        private void SetYRotate(InputAction.CallbackContext ctx)
        {
            previousYRotation = ctx.ReadValue<float>();
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
            if(!Conf.EnabledEdgeMovement) return;

            UpdatePosition(
                EvaluateMovement(EdgeScreenDirectionX(), EdgeScreenDirectionY())
            );
        }

        private void ZoomMovement()
        {
            if(!Conf.EnableZoomMovement) return;

            var zoomInput = (-previousZoomInput).Normalize();

            targetZoom += zoomInput * Conf.ZoomAmount;
            targetZoom = Mathf.Clamp(
                targetZoom,
                Conf.MoveLimitsY.Start != 0f ? Conf.MoveLimitsY.Start : 0f,
                Conf.MoveLimitsY.End != 0f ? Conf.MoveLimitsY.End : float.MaxValue
            );

            var positionY = Mathf.Lerp(
                target.position.y,
                targetZoom,
                Time.deltaTime * Conf.ZoomSpeed
            );

            target.position = new Vector3(
                target.position.x, positionY, target.position.z
            );
        }

        private Vector3 EvaluateMovement(float xDirection, float zDirection)
        {
            var moveDirection = Vector3.forward * zDirection
                + Vector3.right * xDirection;

            return Conf.CameraSpeed * Time.deltaTime * moveDirection.normalized;
        }

        private float EdgeScreenDirectionX()
        {
            var mousePosition = Mouse.current.position.ReadValue();

            if(mousePosition.x > Screen.width - Conf.EdgeOffset)
                return 1f;
            else if(mousePosition.x < Conf.EdgeOffset)
                return -1f;

            return 0f;
        }

        private float EdgeScreenDirectionY()
        {
            var mousePosition = Mouse.current.position.ReadValue();

            if(mousePosition.y > Screen.height - Conf.EdgeOffset)
                return 1f;
            else if(mousePosition.y < Conf.EdgeOffset)
                return -1f;

            return 0f;
        }

        private void UpdatePosition(Vector3 addPosition)
        {
            var newPos = target.position + addPosition;
            newPos.x = Mathf.Clamp(newPos.x, Conf.MoveLimitsX.Start, Conf.MoveLimitsX.End);
            newPos.y = Mathf.Clamp(
                newPos.y,
                Conf.MoveLimitsY.Start != 0f ? Conf.MoveLimitsY.Start : float.MinValue,
                Conf.MoveLimitsY.End != 0f ? Conf.MoveLimitsY.End : float.MaxValue
            );
            newPos.z = Mathf.Clamp(newPos.z, Conf.MoveLimitsZ.Start, Conf.MoveLimitsZ.End);

            target.position = newPos;
        }

    }
}