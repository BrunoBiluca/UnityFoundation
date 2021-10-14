using UnityEngine;

namespace Assets.UnityFoundation.Code.CameraScripts.NewInputSystem
{
    public class CameraMovementXZController : MonoBehaviour
    {
        [SerializeField] private float cameraSpeed = 20f;
        [SerializeField] private float edgeOffset = 10f;
        [SerializeField] private Vector2 moveLimitsX;
        [SerializeField] private Vector2 moveLimitsZ;

        private CameraMovementXZ cameraMovement;

        private void Awake()
        {
            cameraMovement = new CameraMovementXZ(transform) {
                CameraSpeed = cameraSpeed,
                EdgeOffset = edgeOffset,
                MoveLimitsX = moveLimitsX,
                MoveLimitsZ = moveLimitsZ
            };
        }

        private void Update()
        {
            cameraMovement.OnUpdate();
        }


    }
}