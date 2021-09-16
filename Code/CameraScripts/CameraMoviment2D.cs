using Cinemachine;
using UnityEngine;

namespace Assets.UnityFoundation.CameraScripts
{
    public class CameraMoviment2D : MonoBehaviour
    {

        private CinemachineVirtualCamera virtualCamera;

        private float orthographicSize;
        private float targetOrthographicSize;

        private void Awake()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        void Start()
        {
            orthographicSize = virtualCamera.m_Lens.OrthographicSize;
            targetOrthographicSize = orthographicSize;
        }

        void Update()
        {
            var xAxis = Input.GetAxisRaw("Horizontal");
            var yAxis = Input.GetAxisRaw("Vertical");

            var moveDirection = new Vector3(xAxis, yAxis).normalized;
            var moveSpeed = 30f;

            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            var zoomAmount = 2f;
            var zoomMin = 10f;
            var zoomMax = 30f;
            targetOrthographicSize += -Input.mouseScrollDelta.y * zoomAmount;
            targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, zoomMin, zoomMax);

            var zoomSpeed = 5f;
            orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);
            virtualCamera.m_Lens.OrthographicSize = orthographicSize;
        }

    }
}