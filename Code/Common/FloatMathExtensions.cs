namespace Assets.UnityFoundation.Code
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
    }
}