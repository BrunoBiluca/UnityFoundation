namespace UnityFoundation.Code
{
    public static class IntMathExtensions
    {
        public static int Clamp(this int value, int min, int max)
        {
            if(value < min)
                return min;

            if(value > max)
                return max;

            return value;
        }
    }
}