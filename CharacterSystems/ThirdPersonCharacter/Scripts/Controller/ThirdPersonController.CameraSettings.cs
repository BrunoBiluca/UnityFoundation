using System;
using UnityEngine;

namespace UnityFoundation.ThirdPersonCharacter
{
    public partial class ThirdPersonController
    {
        [Serializable]
        public class CameraSettings
        {
            [Tooltip("Camera movement speed")]
            public float Sensitivity = 1f;

            [Tooltip("How far in degrees can you move the camera up")]
            public float TopClamp = 70.0f;

            [Tooltip("How far in degrees can you move the camera down")]
            public float BottomClamp = -30.0f;

            [Tooltip(
                "Additional degress to override the camera. "
                + "Useful for fine tuning camera position when locked"
            )]
            public float CameraAngleOverride = 0.0f;

            [Tooltip("For locking the camera position on all axis")]
            public bool LockCameraPosition = false;
        }
    }
}