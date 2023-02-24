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

        public float InterpolationSpeed { get; set; } = 1f;
        public bool Looping { get; set; } = false;

        public Lerp(float startValue, float endValue)
        {
            this.startValue = startValue;
            this.endValue = endValue;
        }

        public Lerp SetEndValue(float newEndValue)
        {
            startValue = endValue;
            endValue = newEndValue;
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
    }
}