using UnityEngine;

namespace UnityFoundation.Code
{
    /// <summary>
    /// Base linear interpolation feature.
    /// </summary>
    public class Lerp
    {
        private float startValue;
        private float endValue;
        private float currentInterpolationAmount;
        private float range;

        public float InterpolationSpeed { get; set; } = 1f;
        public bool Looping { get; set; } = false;

        public Lerp(float startValue)
        {
            this.startValue = startValue;
            endValue = startValue;
        }

        public Lerp SetEndValue(float newEndValue)
        {
            startValue = endValue;
            endValue = newEndValue;

            var direction = startValue * endValue;
            if(direction < 0f)
                range = Mathf.Abs(startValue) + Mathf.Abs(endValue);
            else
                range = Mathf.Abs(Mathf.Abs(endValue) - Mathf.Abs(startValue));

            return this;
        }

        public void ResetInterpolation()
        {
            currentInterpolationAmount = 0f;
        }

        /// <summary>
        /// Evaluate the interpolation amount by the percentage of the movement.
        /// </summary>
        /// <param name="addPercentage">Percentagem of the movement</param>
        /// <returns>Current interpolation amount</returns>
        public float Eval(float addPercentage)
        {
            currentInterpolationAmount += addPercentage * InterpolationSpeed;

            if(Looping)
                if(currentInterpolationAmount > 1f)
                    ResetInterpolation();

            return Mathf.Lerp(startValue, endValue, currentInterpolationAmount);
        }

        /// <summary>
        /// Evaluate the interpolation amount by the value between the start and end.
        /// </summary>
        /// <param name="value">Value between start and end</param>
        /// <returns>Current interpolation amount</returns>
        public float EvalBy(float value)
        {
            var amount = value / range;
            return Eval(amount);
        }
    }
}