using UnityEngine;

namespace UnityFoundation.Code
{
    public class LerpByValue
    {
        private readonly float startValue;
        private readonly float endValue;
        private readonly float range;
        private float currentInterpolationAmount;

        public float InterpolationSpeed { get; set; } = 1f;
        public bool Looping { get; set; } = false;

        public LerpByValue(float startValue, float endValue)
        {
            this.startValue = startValue;
            this.endValue = endValue;

            var direction = startValue * endValue;
            if(direction < 0f)
                range = Mathf.Abs(startValue) + Mathf.Abs(endValue);
            else
                range = Mathf.Abs(Mathf.Abs(endValue) - Mathf.Abs(startValue));
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