using UnityFoundation.Code;
using UnityEngine;

namespace UnityFoundation.CarSystem
{
    public class CarWheelsController : MonoBehaviour
    {
        [SerializeField] private Transform frontWheelLeft;
        [SerializeField] private Transform frontWheelRight;
        [SerializeField] private Transform rearWheelLeft;
        [SerializeField] private Transform rearWheelRight;

        private LerpByValue wheelsInterpolation;

        private CarInputs inputs;

        private Rigidbody rg;

        private float rotateX;

        void Awake()
        {
            inputs = GetComponent<CarInputs>();
            rg = GetComponent<Rigidbody>();
            wheelsInterpolation = new LerpByValue(0f, 0f) { InterpolationSpeed = 5f };
        }

        void Update()
        {
            rotateX += RotateXWheels();
            var rotateY = TurnWheels();

            var rotateFront = Quaternion.Euler(
                rotateX * rg.velocity.magnitude,
                rotateY,
                0f
            );

            var rotateBack = Quaternion.Euler(rotateX * rg.velocity.magnitude, 0f, 0f);

            frontWheelLeft.localRotation = rotateFront;
            frontWheelRight.localRotation = rotateFront;
            rearWheelLeft.localRotation = rotateBack;
            rearWheelRight.localRotation = rotateBack;
        }

        private float RotateXWheels()
        {
            if(Vector3Utils.NearlyEqual(rg.velocity, Vector3.zero, 0.3f))
            {
                rotateX = 0f;
                return 0f;
            }


            if(Vector3.Dot(rg.velocity, transform.forward) < 0)
                return -1f;

            return 1f;
        }

        private float TurnWheels()
        {
            var wheelsRotations = 0f;

            if(inputs.Turn > 0f)
                wheelsRotations = 45f;

            if(inputs.Turn < 0f)
                wheelsRotations = -45f;

            var interpolation = wheelsInterpolation
                .SetEndValue(wheelsRotations)
                .Eval(Time.deltaTime);

            return interpolation;
        }
    }
}