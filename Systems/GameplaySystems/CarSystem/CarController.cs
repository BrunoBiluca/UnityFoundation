using UnityEngine;
using Cinemachine;
using UnityFoundation.Code;

namespace UnityFoundation.CarSystem
{
    public partial class CarController : MonoBehaviour
    {
        [SerializeField] private float turnSpeed;
        [SerializeField] private float speed;
        [SerializeField] private float topSpeed;

        [SerializeField] private CinemachineVirtualCamera vCamera;
        private CinemachineComposer vCameraComposer;
        private Lerp cameraScreenX;

        private Rigidbody rb;
        private BoxCollider boxCollider;
        private CarInputs inputs;

        private bool isGrounded;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();
            inputs = GetComponent<CarInputs>();
        }

        private void Start()
        {
            vCameraComposer = vCamera.GetCinemachineComponent<CinemachineComposer>();
            cameraScreenX = new Lerp(vCameraComposer.m_ScreenX, 0.5f);
        }

        private void Update()
        {
            EvaluateCameraOffset();
        }

        private void EvaluateCameraOffset()
        {
            var screenX = 0.5f;

            if(inputs.Turn > 0f)
                screenX = .3f;

            if(inputs.Turn < 0f)
                screenX = .7f;

            vCameraComposer.m_ScreenX = cameraScreenX.SetEndValue(screenX).Eval(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            CheckGround();

            if(!isGrounded) return;

            var newRotation = inputs.Turn * turnSpeed * Time.deltaTime;
            transform.Rotate(0, newRotation, 0, Space.World);

            if(rb.velocity.magnitude >= topSpeed)
                return;

            rb.AddForce(
                transform.forward * inputs.Move * speed,
                ForceMode.Acceleration
            );
        }

        private void CheckGround()
        {
            isGrounded = Physics.Raycast(
                boxCollider.bounds.center,
                transform.Down(),
                boxCollider.size.y + 0.1f
            );

            if(isGrounded)
                Debug.DrawRay(
                    boxCollider.bounds.center,
                    transform.Down() * (boxCollider.size.y + 0.1f),
                    Color.green
                );
            else
                Debug.DrawRay(
                    boxCollider.bounds.center,
                    transform.Down() * (boxCollider.size.y + 0.1f),
                    Color.red
                );
        }
    }
}