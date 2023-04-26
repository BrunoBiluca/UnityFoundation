using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.CameraMovementXZ
{
    public class CameraXZController : MonoBehaviour
    {
        [SerializeField] private CameraMovementSettings Settings;

        [SerializeField] private Transform cameraTarget;

        private TransformUpdater cameraMovement;
        private CameraMovementInputActions controls;
        private EdgeScreenInputs edgeInputs;

        public void Awake()
        {
            controls = new CameraMovementInputActions();

            var target = cameraTarget != null ? cameraTarget : transform;

            cameraMovement = new TransformUpdater(target.Decorate());

            foreach(var updater in CreateTransformUpdaters())
                cameraMovement.AddUpdater(updater);

            controls.Enable();
        }

        private IEnumerable<ITransformUpdater> CreateTransformUpdaters()
        {
            yield return CreateAxisMovement();

            if(Settings.EnableEdgeMovement)
            {
                edgeInputs = new EdgeScreenInputs(
                    new EdgeScreenInputs.Settings() { EdgeOffset = Settings.EdgeOffset }
                );
                yield return EnableEdgeMovement();
            }

            if(Settings.EnableZoom)
                yield return EnableZoom();

            if(Settings.EnabledYRotation)
            {
                if(Settings.FixedYRotation > 0f)
                    yield return YFixedRotation();
                else
                    yield return YRotation();
            }
        }

        public void Update()
        {
            edgeInputs?.Update();
            cameraMovement.Update(Time.deltaTime);
        }

        private ITransformUpdater YFixedRotation()
        {
            var rotationUpdater = new WorldFixedRotation(
                new WorldFixedRotation.Settings() {
                    Amount = Settings.FixedYRotation,
                    Speed = Settings.YRotationSpeed,
                    LimitsY = Settings.MoveLimitsY
                },
                transform.rotation.eulerAngles.y
            );

            controls.Camera.YRotate.performed
                += ctx => rotationUpdater.SetYRotation(ctx.ReadValue<float>());
            controls.Camera.YRotate.canceled
                += ctx => rotationUpdater.SetYRotation(ctx.ReadValue<float>());

            return rotationUpdater;
        }

        private ITransformUpdater YRotation()
        {
            var rotationUpdater = new WorldRotation(
                new WorldRotation.Settings() {
                    Speed = Settings.YRotationSpeed,
                    LimitsY = Settings.MoveLimitsY
                }
            );

            controls.Camera.YRotate.performed
                += ctx => rotationUpdater.SetYRotation(ctx.ReadValue<float>());
            controls.Camera.YRotate.canceled
                += ctx => rotationUpdater.SetYRotation(ctx.ReadValue<float>());

            return rotationUpdater;
        }

        private ITransformUpdater EnableEdgeMovement()
        {
            var edgeMovement = new AxisMovement(new AxisMovement.Settings() {
                Speed = Settings.CameraSpeed,
                LimitsX = Settings.MoveLimitsX,
                LimitsY = Settings.MoveLimitsY,
                LimitsZ = Settings.MoveLimitsZ
            });

            edgeInputs.OnEdgeMovement += (input) => {
                edgeMovement.SetXDirection(input.x);
                edgeMovement.SetZDirection(input.y);
            };

            return edgeMovement;
        }

        private ITransformUpdater CreateAxisMovement()
        {
            var axisMovement = new AxisMovement(new AxisMovement.Settings() {
                Speed = Settings.CameraSpeed,
                LimitsX = Settings.MoveLimitsX,
                LimitsY = Settings.MoveLimitsY,
                LimitsZ = Settings.MoveLimitsZ
            });

            controls.Camera.AxisMovement.performed += (ctx) => {
                var input = ctx.ReadValue<Vector2>();
                axisMovement.SetXDirection(input.x);
                axisMovement.SetZDirection(input.y);
            };
            controls.Camera.AxisMovement.canceled += (ctx) => {
                var input = ctx.ReadValue<Vector2>();
                axisMovement.SetXDirection(input.x);
                axisMovement.SetZDirection(input.y);
            };

            return axisMovement;
        }

        private ITransformUpdater EnableZoom()
        {
            var zoomMovement = new ZoomMovement(
                new ZoomMovement.Settings() {
                    Speed = Settings.ZoomSpeed,
                    IncrementalAmount = Settings.ZoomAmount,
                    XRotationAngle = Settings.XRotationAngle,
                    LimitsY = Settings.MoveLimitsY
                },
                transform.position.y
            );
            controls.Camera.Zoom.performed
                += (ctx) => zoomMovement.SetZoomValue(ctx.ReadValue<float>());
            controls.Camera.Zoom.canceled
                += (ctx) => zoomMovement.SetZoomValue(ctx.ReadValue<float>());

            return zoomMovement;
        }
    }
}