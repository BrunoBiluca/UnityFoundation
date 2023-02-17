using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.CameraMovementXZ
{
    public class WorldFixedRotation : ITransformUpdater
    {
        public class Settings
        {
            public float Amount { get; set; }
            public float Speed { get; set; }
            public Rangef LimitsY { get; set; } = new Rangef(0f, float.MaxValue);
        }

        private readonly Settings Config;

        private float rotateDirection;
        private bool isEnabled;
        private readonly LerpAngle lerpYRotation;

        public WorldFixedRotation(Settings config, float initialYRotation)
        {
            Config = config;
            lerpYRotation = new LerpAngle(initialYRotation).SetInterpolationSpeed(Config.Speed);
        }

        public void SetYRotation(float value)
        {
            if(isEnabled) return;

            rotateDirection = value.Normalize();
            lerpYRotation.IncreaseAngle(rotateDirection * Config.Amount);
            isEnabled = true;
        }

        public void Update(ITransform transform, float amount)
        {
            if(!isEnabled) return;

            var positionY = lerpYRotation.EvalAngle(amount);
            transform.SetRotation(transform.Rotation.eulerAngles.WithY(positionY));

            isEnabled = !lerpYRotation.ReachedTargetAngle;
        }
    }
}