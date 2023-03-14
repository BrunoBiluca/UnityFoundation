namespace UnityFoundation.Code
{
    public static class UIntMathExtensions
    {
        public static uint Clamp(this uint value, uint min, uint max)
        {
            if(value < min)
                return min;

            if(value > max)
                return max;

            return value;
        }
    }
}
