using UnityEngine;
using UnityFoundation.Code;

namespace UnityFoundation.CameraMovementXZ
{
    public class CameraMovementXZController : MonoBehaviour
    {
        [Header("Movement attributes")]
        [SerializeField] private float cameraSpeed = 20f;
        [SerializeField] private Rangef moveLimitsX;
        [SerializeField] private Rangef moveLimitsY;
        [SerializeField] private Rangef moveLimitsZ;

        [Header("Edge Movement")]
        [SerializeField] private bool enableEdgeMovement = false;
        [SerializeField] private float edgeOffset = 10f;

        [Header("Zoom attributes")]
        [SerializeField] private bool enableZoom = false;
        [SerializeField] private float zoomAmount = 2f;
        [SerializeField] private float zoomSpeed = 5f;

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