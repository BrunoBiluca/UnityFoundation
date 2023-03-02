using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Code.Math;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.CameraMovementXZ
{
    public class AxisMovement : ITransformUpdater
    {
        public class Settings
        {
            public float Speed { get; set; }
            public Rangef LimitsX { get; set; } = new Rangef(0f, float.MaxValue);
            public Rangef LimitsY { get; set; } = new Rangef(0f, float.MaxValue);
            public Rangef LimitsZ { get; set; } = new Rangef(0f, float.MaxValue);
        }

        private Settings Config { get; set; }
        private float zDirection;
        private float xDirection;

        public AxisMovement(Settings config)
        {
            Config = config;
        }

        public void SetXDirection(float value) => xDirection = value;

        public void SetZDirection(float value) => zDirection = value;

        public void Update(ITransform transform, float amount)
        {
            var forward = transform.Forward.WithY(0f);
            var right = transform.Right.WithY(0f);
            var moveDirection = forward * zDirection + right * xDirection;

            var addPosition = Config.Speed * amount * moveDirection.normalized;

            var newPos = transform.Position + addPosition;
            newPos.x = Mathf.Clamp(newPos.x, Config.LimitsX.Start, Config.LimitsX.End);
            newPos.y = Mathf.Clamp(newPos.y, Config.LimitsY.Start, Config.LimitsY.End);
            newPos.z = Mathf.Clamp(newPos.z, Config.LimitsZ.Start, Config.LimitsZ.End);

            transform.Position = newPos;
        }
    }
}