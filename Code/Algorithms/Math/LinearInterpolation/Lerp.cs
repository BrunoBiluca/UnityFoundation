using UnityEngine;

namespace UnityFoundation.Code
{
    public class Lerp
    {
        private float startValue;
        private float endValue;
        private float currentInterpolationAmount;
        private float interpolationSpeed;
        private float range;

        public float BaseValue => endValue;
        public float InterpolationSpeed => interpolationSpeed;

        public Lerp(float startValue)
        {
            this.startValue = startValue;
            endValue = startValue;
            interpolationSpeed = 1f;
        }

        public Lerp SetEndValue(float newEndValue)
        {
            startValue = endValue;
            endValue = newEndValue;
            currentInterpolationAmount = 0f;

            var direction = startValue * endValue;
            if(direction < 0f)
                range = Mathf.Abs(startValue) + Mathf.Abs(endValue);
            else
                range = Mathf.Abs(Mathf.Abs(endValue) - Mathf.Abs(startValue));

            return this;
        }

        public Lerp SetInterpolationSpeed(float newInterpolationSpeed)
        {
            interpolationSpeed = newInterpolationSpeed;
            return this;
        }

        public float Eval(float amount)
        {
            currentInterpolationAmount += amount;
            return Mathf.Lerp(
                startValue,
                endValue,
                currentInterpolationAmount * interpolationSpeed
            );
        }

        public float EvalBy(float value)
        {
            var amount = value / range;
            return Eval(amount);
        }
    }
}