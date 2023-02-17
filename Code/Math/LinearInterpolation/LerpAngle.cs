using UnityEngine;

namespace UnityFoundation.Code
{
    public class LerpAngle
    {
        private float startValue;
        private float endValue;

        private float currentValue;
        private float currentInterpolationAmount;
        private float interpolationSpeed;
        private float range;

        public float StartValue => startValue;
        public float EndValue => endValue;
        public float InterpolationSpeed => interpolationSpeed;

        /// <summary>
        /// When enabled evaluate the end value as the closest angle 
        /// in the 360 degrees circle from the start value
        /// </summary>
        public bool CheckMinPath { get; set; } = true;

        public bool RetainState { get; set; } = false;

        public bool ReachedTargetAngle { get; set; }

        public LerpAngle(float startValue)
        {
            this.startValue = startValue;
            currentValue = startValue;
            endValue = startValue;
            interpolationSpeed = 1f;
        }

        public LerpAngle SetInterpolationSpeed(float newInterpolationSpeed)
        {
            interpolationSpeed = newInterpolationSpeed;
            return this;
        }

        public LerpAngle IncreaseAngle(float amount)
        {
            return SetEndValue(currentValue + amount);
        }

        public LerpAngle SetEndValue(float newEndValue)
        {
            startValue = endValue;

            if(RetainState)
                startValue = currentValue;

            endValue = newEndValue;
            currentInterpolationAmount = 0f;
            ReachedTargetAngle = false;

            if(CheckMinPath)
                EvaluateMinPath();

            range = MathX.Distance(startValue, endValue);

            return this;
        }

        private void EvaluateMinPath()
        {
            if(!startValue.IsBetween(-360f, 360f) || !endValue.IsBetween(-360f, 360f))
                return;

            var mappedStartValue = startValue >= 0f ? startValue - 360f : 360f + startValue;
            var mappedEndValue = endValue >= 0f ? endValue - 360f : 360f + endValue;

            var originalDistance = MathX.Distance(startValue, endValue);
            var mappedDistance = MathX.Distance(startValue, mappedEndValue);
            if(mappedDistance < originalDistance)
                endValue = mappedEndValue;

            var mappedStartDistance = MathX.Distance(mappedStartValue, endValue);
            if(mappedStartDistance < originalDistance)
                startValue = mappedStartValue;
        }

        public float EvalAngle(float value)
        {
            var amount = value / range;
            currentInterpolationAmount += amount;

            currentValue = Mathf.Lerp(
                startValue,
                endValue,
                currentInterpolationAmount * interpolationSpeed
            );

            if(currentValue.NearlyEqual(endValue, 0.001f))
            {
                currentValue = endValue;
                ReachedTargetAngle = true;
            }

            return currentValue;
        }
    }
}