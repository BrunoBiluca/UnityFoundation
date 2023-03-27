using UnityEngine;

namespace UnityFoundation.Code
{
    public class LerpByValue
    {
        private float startValue;
        private float endValue;
        private float range;
        private float currentInterpolationAmount;

        public float InterpolationSpeed { get; set; } = 1f;
        public bool Looping { get; set; } = false;

        public LerpByValue(float startValue, float endValue)
        {
            this.startValue = startValue;
            this.endValue = endValue;
            range = MathX.Distance(startValue, endValue);
        }

        public LerpByValue SetEndValue(float newEndValue)
        {
            startValue = endValue;
            endValue = newEndValue;
            range = MathX.Distance(startValue, endValue);
            return this;
        }

        public void ResetInterpolation()
        {
            currentInterpolationAmount = 0f;
        }

        /// <summary>
        /// Evaluate the interpolation increase by an amount between start and end values.
        /// </summary>
        public float Eval(float value)
        {
            var amount = value / range;
            currentInterpolationAmount += amount * InterpolationSpeed;

            if(Looping)
                if(currentInterpolationAmount > 1f)
                    ResetInterpolation();

            return Mathf.Lerp(startValue, endValue, currentInterpolationAmount);
        }
    }
}