using UnityEngine;

namespace UnityFoundation.Code
{
    /// <summary>
    /// Linear intepolate an angle given start and end values.
    /// </summary>
    public class LerpAngle
    {
        private float startValue;
        private float endValue;

        private float currentValue;
        private float currentInterpolationAmount;
        private float range;

        public float StartValue => startValue;
        public float EndValue => endValue;
        public float InterpolationSpeed { get; set; } = 1f;

        /// <summary>
        /// When enabled evaluate the end value as the closest angle 
        /// in the 360 degrees circle from the start value
        /// </summary>
        public bool CheckMinPath { get; set; } = true;
        /// <summary>
        /// Update linear interpolation state. Start value is updated to current value.
        /// </summary>
        public bool RetainState { get; set; } = false;
        public bool Looping { get; set; } = false;

        public bool IsTargetReached { get; private set; }


        public LerpAngle(float startValue)
        {
            this.startValue = startValue;
            currentValue = startValue;
            endValue = startValue;
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
            IsTargetReached = false;

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
            currentInterpolationAmount += amount * InterpolationSpeed;

            currentValue = Mathf.Lerp(startValue, endValue, currentInterpolationAmount);

            if(currentValue.NearlyEqual(endValue, 0.001f))
            {
                currentValue = endValue;
                IsTargetReached = true;
            }

            if(Looping && currentInterpolationAmount >= 1f)
                currentInterpolationAmount = 0f;

            return currentValue;
        }
    }
}