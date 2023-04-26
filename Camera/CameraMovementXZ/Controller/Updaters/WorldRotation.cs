using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.CameraMovementXZ
{
    public class WorldRotation : ITransformUpdater
    {
        public class Settings
        {
            public float Speed { get; set; }
            public Rangef LimitsY { get; set; } = new Rangef(0f, float.MaxValue);
        }

        public Settings Config { get; set; }

        private float yRotation;

        public WorldRotation(Settings config)
        {
            Config = config;
        }

        public void SetYRotation(float value) => yRotation = value;

        public void Update(ITransform transform, float amount)
        {
            var rotationY = yRotation * Config.Speed * amount;
            transform.RotateOnWorld(new Vector3(0f, rotationY, 0f));
        }
    }
}