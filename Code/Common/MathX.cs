using UnityEngine;

namespace UnityFoundation.Code
{
    public static class MathX
    {
        public static float Remainder(float value) => Mathf.Floor(value) - value;

        public static bool NearlyEqual(float a, float b, float epsilon)
        {
            float absA = Mathf.Abs(a);
            float absB = Mathf.Abs(b);
            float diff = Mathf.Abs(a - b);

            if(a == b)
                return true;
            else if(a == 0 || b == 0 || diff < float.Epsilon)
                return diff < epsilon;
            else
                return diff / (absA + absB) < epsilon;
        }

        public static float Remap(
            float value,
            float from1,
            float to1,
            float from2,
            float to2,
            bool clampValue = true
        )
        {
            var testValue = value;

            if(clampValue)
                testValue = Mathf.Clamp(value, from1, to1);

            return (testValue - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        public static float Borders(float value, float borderMin, float borderMax)
        {
            if(value == 0f) return 0f;

            return value > 0f ? borderMax : borderMin;
        }

        public static bool IsBetween(float value, float minInclusive, float maxInclusive)
        {
            return minInclusive <= value && value <= maxInclusive;
        }

        public static float Distance(float a, float b)
        {
            if(a * b >= 0f)
                return Mathf.Abs(Mathf.Abs(a) - Mathf.Abs(b)); 

            var distanceA = Distance(a, 0f);
            var distanceB = Distance(b, 0f);
            return distanceA + distanceB;
        }
    }
}