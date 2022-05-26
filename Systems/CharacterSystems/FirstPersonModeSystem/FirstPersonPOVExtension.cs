using Cinemachine;
using UnityEngine;

namespace UnityFoundation.FirstPersonModeSystem
{
    public class FirstPersonPOVExtension : CinemachineExtension
    {
        [SerializeField] private float clampViewY = 80f;
        [SerializeField] private bool invertY = false;

        private FirstPersonInputs inputs;

        private Vector3 startingRot;

        public void Init(FirstPersonInputs inputs)
        {
            this.inputs = inputs;
        }

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage,
            ref CameraState state,
            float deltaTime
        )
        {
            if(vcam.Follow == null) return;
            if(stage != CinemachineCore.Stage.Aim) return;
            if(inputs == null) return;

            if(startingRot == null)
                startingRot = transform.localRotation.eulerAngles;

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