using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnityFoundation.Code
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
    }

    public static class FloatMathExtensions
    {
        public static float Remainder(this float value) => Mathf.Floor(value) - value;
    }
}