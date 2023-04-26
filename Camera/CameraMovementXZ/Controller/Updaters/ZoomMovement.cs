using System;
using UnityEngine;
using UnityFoundation.Code;
using UnityFoundation.Code.Math;
using UnityFoundation.Code.UnityAdapter;

namespace UnityFoundation.CameraMovementXZ
{
    public class ZoomMovement : ITransformUpdater
    {
        public class Settings
        {
            public float IncrementalAmount { get; set; } = 2f;
            public float Speed { get; set; } = 5f;
            public Rangef LimitsY { get; set; } = new Rangef(0f, float.MaxValue);
            public Rangef XRotationAngle { get; set; } = new Rangef(45f, 45f);
        }

        private Settings Config { get; }

        private float ZoomValue => (-zoomValue).Normalize();

        private bool isEnabled = false;
        private float zoomValue;
        private float targetZoom;
        private float targetXAngle;

        public ZoomMovement(Settings config, float initialValue)
        {
            Config = config;
            targetZoom = initialValue;
        }

        public void SetZoomValue(float zoomValue)
        {
            this.zoomValue = zoomValue;
            isEnabled = true;
        }

        public void Update(ITransform transform, float amount)
        {
            if(!isEnabled) return;

            targetZoom += ZoomValue * Config.IncrementalAmount;
            targetZoom = Mathf.Clamp(targetZoom, Config.LimitsY.Start, Config.LimitsY.End);

            var positionY = Mathf.Lerp(transform.Position.y, targetZoom, amount * Config.Speed);
            transform.Position = transform.Position.WithY(positionY);

            UpdateXRotation(transform);

            if(targetZoom.NearlyEqual(ZoomValue, 0.01f))
                isEnabled = false;
        }

        private void UpdateXRotation(ITransform transform)
        {
            targetXAngle = MathX.Remap(
                transform.Position.y,
                Config.LimitsY.Start, 
                Config.LimitsY.End,
                Config.XRotationAngle.Start, 
                Config.XRotationAngle.End
            );

            transform.SetRotation(new Vector3(
                targetXAngle,
                transform.Rotation.eulerAngles.y,
                transform.Rotation.eulerAngles.z
            ));
        }
    }
}