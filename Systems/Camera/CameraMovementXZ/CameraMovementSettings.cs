using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.CameraMovementXZ
{
    [CreateAssetMenu(
        menuName = UnityFoundationEditorConfig.BASE_CONTEXT_MENU_PATH + "Camera XZ/Config",
        fileName = "camera_xz_config"
    )]
    public class CameraMovementSettings : ScriptableObject
    {
        [field: Header("Movement attributes")]

        [field: SerializeField]
        public float CameraSpeed { get; private set; } = 20f;

        [field: SerializeField]
        [Tooltip("Limits in the X axis")]
        public Rangef MoveLimitsX { get; private set; }

        [field: SerializeField]
        [Tooltip("Limits in the Y axis")]
        public Rangef MoveLimitsY { get; private set; }

        [field: SerializeField]
        [Tooltip("Limits in the Z axis")]
        public Rangef MoveLimitsZ { get; private set; }

        [field: Header("Edge Movement")]

        [field: SerializeField]
        [Tooltip("Enable movement when mouse cursor is on the edge of the screen")]
        public bool EnableEdgeMovement { get; private set; } = false;

        [field: SerializeField]
        [Tooltip("Edge size for move camera")]
        public float EdgeOffset { get; private set; } = 10f;

        [field: Header("Zoom attributes")]

        [field: SerializeField]
        [Tooltip("Enable movement in the Y axis")]
        public bool EnableZoom { get; private set; } = false;

        [field: SerializeField]
        [Tooltip("Zoom's sensibility")]
        public float ZoomAmount { get; private set; } = 2f;

        [field: SerializeField]
        [Tooltip("Zoom's speed")]
        public float ZoomSpeed { get; private set; } = 5f;

        
        [field: Header("X Rotation attributes")]

        [field: SerializeField]
        public bool EnabledXRotation { get; set; } = true;
        [field: SerializeField]
        public Rangef XRotationAngle { get; set; }


        [field: Header("Y Rotation attributes")]

        [field: SerializeField]
        public bool EnabledYRotation { get; set; } = true;
        [field: SerializeField]
        public float YRotationSpeed { get; set; } = 10f;
    }
}