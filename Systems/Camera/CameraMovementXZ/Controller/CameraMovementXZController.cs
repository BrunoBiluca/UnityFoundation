using UnityEngine;

namespace UnityFoundation.CameraMovementXZ
{
    public class CameraMovementXZController : MonoBehaviour
    {
        [SerializeField] private CameraMovementSettings Settings;

        [SerializeField] private Transform cameraTarget;

        private CameraMovementXZ cameraMovement;

        private void Awake()
        {
            var target = cameraTarget != null ? cameraTarget : transform;

            cameraMovement = new CameraMovementXZ(target);
            cameraMovement.Conf.CameraSpeed = Settings.CameraSpeed;
            cameraMovement.Conf.EdgeOffset = Settings.EdgeOffset;
            cameraMovement.Conf.MoveLimitsX = Settings.MoveLimitsX;
            cameraMovement.Conf.MoveLimitsZ = Settings.MoveLimitsZ;

            cameraMovement.Conf.MoveLimitsY = Settings.MoveLimitsY;
            cameraMovement.Conf.EnabledEdgeMovement = Settings.EnableEdgeMovement;

            cameraMovement.Conf.EnableZoomMovement = Settings.EnableZoom;
            cameraMovement.Conf.ZoomAmount = Settings.ZoomAmount;
            cameraMovement.Conf.ZoomSpeed = Settings.ZoomSpeed;

            cameraMovement.Conf.EnabledYRotation = Settings.EnabledYRotation;
            cameraMovement.Conf.YRotationSpeed = Settings.YRotationSpeed;

            cameraMovement.Conf.EnabledXRotation = Settings.EnabledYRotation;
            cameraMovement.Conf.XRotationAngle = Settings.XRotationAngle;
        }

        private void Update()
        {
            cameraMovement.OnUpdate();
        }
    }
}