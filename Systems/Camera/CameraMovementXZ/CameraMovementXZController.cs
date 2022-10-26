using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.CameraMovementXZ
{
    public class CameraMovementXZController : MonoBehaviour
    {
        [Header("Movement attributes")]

        [SerializeField]
        private float cameraSpeed = 20f;

        [SerializeField]
        [Tooltip("Limits in the X axis")]
        private Rangef moveLimitsX;

        [SerializeField]
        [Tooltip("Limits in the Y axis")]
        private Rangef moveLimitsY;

        [SerializeField]
        [Tooltip("Limits in the Z axis")]
        private Rangef moveLimitsZ;

        [Header("Edge Movement")]

        [SerializeField] 
        [Tooltip("Enable movement when mouse cursor is on the edge of the screen")]
        private bool enableEdgeMovement = false;

        [SerializeField]
        [Tooltip("Edge size for move camera")]
        private float edgeOffset = 10f;

        [Header("Zoom attributes")]

        [SerializeField]
        [Tooltip("Enable movement in the Y axis")]
        private bool enableZoom = false;

        [SerializeField]
        [Tooltip("Zoom's sensibility")]
        private float zoomAmount = 2f;

        [SerializeField] 
        [Tooltip("Zoom's speed")]
        private float zoomSpeed = 5f;

        private CameraMovementXZ cameraMovement;

        private void Awake()
        {
            cameraMovement = new CameraMovementXZ(transform) {
                CameraSpeed = cameraSpeed,
                EdgeOffset = edgeOffset,
                MoveLimitsX = moveLimitsX,
                MoveLimitsZ = moveLimitsZ
            };

            cameraMovement.MoveLimitsY = moveLimitsY;
            cameraMovement.EnabledEdgeMovement = enableEdgeMovement;

            cameraMovement.EnableZoomMovement = enableZoom;
            cameraMovement.ZoomAmount = zoomAmount;
            cameraMovement.ZoomSpeed = zoomSpeed;
        }

        private void Update()
        {
            cameraMovement.OnUpdate();
        }
    }
}