using UnityEngine;

namespace Assets.UnityFoundation.Code
{
    public class Lerp
    {
        private float baseValue;
        private float current;
        private float interpolationSpeed;

        public Lerp(float baseValue)
        {
            this.baseValue = baseValue;
            current = baseValue;
            interpolationSpeed = 1f;
        }

        public Lerp SetBase(float newBaseValue)
        {
            baseValue = newBaseValue;
            return this;
        }

        public Lerp SetInterpolationSpeed(float newInterpolationSpeed)
        {
            interpolationSpeed = newInterpolationSpeed;
            return this;
        }

        public float Eval(float amount)
        {
            current = Mathf.Lerp(
                current, baseValue, amount * interpolationSpeed
            );

            return current;
        }
    }
}