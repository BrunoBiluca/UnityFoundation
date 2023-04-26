using Cinemachine;
using UnityEngine;

namespace UnityFoundation.Camera.FreeCamera
{
    public class FreeCameraCinemachineExtension : CinemachineExtension
    {
        [SerializeField] private float clampViewY = 80f;
        [SerializeField] private float cameraMovementSpeed = 10f;
        [SerializeField] private bool invertY = false;

        private FreeCameraInputs inputs;

        private Vector3 startingPos;
        private Vector3 startingRot;

        protected override void OnEnable()
        {
            startingPos = transform.position;
            startingRot = transform.localRotation.eulerAngles;

            var inputActions = new FreeCameraInputActions();
            inputs = new FreeCameraInputs(inputActions);
            inputs.Enable();
        }

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage,
            ref CameraState state,
            float deltaTime
        )
        {
            if(stage != CinemachineCore.Stage.Aim) return;
            if(inputs == null) return;
            if(!inputs.IsLocked) return;

            EvaluateRotation(ref state);
            EvaluatePosition(ref state, deltaTime);
        }

        private void EvaluatePosition(ref CameraState state, float deltaTime)
        {
            var forward = state.RawOrientation * Vector3.forward;
            var right = state.RawOrientation * Vector3.right;

            var targetDirection = cameraMovementSpeed * deltaTime
                * new Vector3(inputs.Move.x, 0f, inputs.Move.y).normalized;

            var newPos = forward * targetDirection.z + right * targetDirection.x;

            startingPos += newPos;

            state.RawPosition = startingPos;
        }

        private void EvaluateRotation(ref CameraState state)
        {
            var deltaRot = inputs.Look * Time.deltaTime;

            startingRot.x += deltaRot.x;

            if(invertY)
                startingRot.y += deltaRot.y;
            else
                startingRot.y -= deltaRot.y;

            startingRot.y = Mathf.Clamp(startingRot.y, -clampViewY, clampViewY);

            state.RawOrientation = Quaternion.Euler(new Vector3(startingRot.y, startingRot.x, 0f));
        }
    }
}
