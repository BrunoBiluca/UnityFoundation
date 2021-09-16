using UnityEngine;

namespace Assets.UnityFoundation.Code.CameraScripts
{
    class CameraMoviment3D : MonoBehaviour
    {
        [SerializeField] private float cameraSpeed = 30f;

        [SerializeField] private float zoomMin = 1f;
        [SerializeField] private float zoomMax = 10f;
        [SerializeField] private float zoomSpeed = 5f;
        [SerializeField] private float zoomAmount = 2f;

        private float targetZoom;

        private void Start()
        {
            targetZoom = transform.position.y;
        }

        void Update()
        {
            var xAxis = Input.GetAxisRaw("Horizontal");
            var yAxis = Input.GetAxisRaw("Vertical");

            var moveDirection = new Vector3(xAxis, 0f, yAxis).normalized;
            transform.position += moveDirection * cameraSpeed * Time.deltaTime;

            targetZoom += -Input.mouseScrollDelta.y * zoomAmount;
            targetZoom = Mathf.Clamp(targetZoom, zoomMin, zoomMax);

            var positionY = Mathf.Lerp(
                transform.position.y,
                targetZoom,
                Time.deltaTime * zoomSpeed
            );
            transform.position = new Vector3(
                transform.position.x, positionY, transform.position.z
            );
        }
    }
}
