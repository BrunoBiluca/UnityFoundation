namespace UnityFoundation.Code
{
    public static class FloatMathExtensions
    {
        public static float Remainder(this float value) => MathX.Remainder(value);

        public static float Remap(
            this float value,
            float from1,
            float to1,
            float from2,
            float to2
        )
            => MathX.Remap(value, from1, to1, from2, to2);

        public static float Normalize(this float value)
        {
            if(value > 0f) return 1f;
            if(value < 0f) return -1;
            return 0f;
        }

        public static bool NearlyEqual(this float value, float expected, float epsilon)
            => MathX.NearlyEqual(value, expected, epsilon);

        public static bool NearlyEqualOrLess(this float value, float expected, float epsilon)
            => value < expected || MathX.NearlyEqual(value, expected, epsilon);

        public static bool NearlyEqualOrGreater(this float value, float expected, float epsilon)
            => value > expected || MathX.NearlyEqual(value, expected, epsilon);

        public static bool IsBetween(this float value, float minInclusive, float maxInclusive)
            => MathX.IsBetween(value, minInclusive, maxInclusive);
        public static bool IsBetweenWithoutOrder(
            this float value, float aInclusive, float bInclusive
        )
            => MathX.IsBetweenWithoutOrder(value, aInclusive, bInclusive);

        public static float Borders(this float value, float borderMin, float borderMax)
            => MathX.Borders(value, borderMin, borderMax);
    }
}