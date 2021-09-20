using UnityEngine;

namespace Assets.UnityFoundation.Code.CameraScripts
{
    /// <summary>
    /// <para>
    ///   Movimentação de câmera com perspectiva top down.
    /// </para>
    /// <para>
    ///   Inputs:<br/>
    ///   - Axis Horizontal, Vertical para plano XZ<br/>
    ///   - Scroll mouse para eixo Y<br/>
    ///   - Keys: Q,E para rotação
    /// </para>
    /// </summary>
    class CameraMovement3D : MonoBehaviour
    {
        [SerializeField] private float cameraSpeed = 30f;

        [SerializeField] private float rotationSpeed = 20f;

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
            AxisMovement();
            ScrollMovement();
            RotationMovement();
        }

        private void AxisMovement()
        {
            var xAxis = Input.GetAxisRaw("Horizontal");
            var zAxis = Input.GetAxisRaw("Vertical");

            var forward = transform.forward;
            forward.y = 0f;
            var moveDirection = (forward * zAxis + transform.right * xAxis);

            transform.position += moveDirection.normalized * cameraSpeed * Time.deltaTime;
        }

        private void ScrollMovement()
        {
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

        private void RotationMovement()
        {
            var rotDirection = 0;

            if(Input.GetKey(KeyCode.Q))
                rotDirection = -1;

            if(Input.GetKey(KeyCode.E))
                rotDirection = 1;

            transform.Rotate(
                new Vector3(
                0f,
                Time.deltaTime * rotDirection * rotationSpeed,
                0f),
                Space.World
            );
        }
    }
}
